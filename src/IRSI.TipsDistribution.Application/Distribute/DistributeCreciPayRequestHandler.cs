using System.Diagnostics;
using System.IO.Compression;
using IRSI.CommonTools.Abstractions;
using IRSI.CommonTools.Abstractions.FileSystem;
using IRSI.TipsDistribution.Application.Contracts;
using IRSI.TipsDistribution.Application.Options;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace IRSI.TipsDistribution.Application.Distribute;

public class DistributeCreciPayRequestHandler : IRequestHandler<DistributeCreciPayRequest>
{
    private const string IBERDIR = "IBERDIR";

    private readonly IEnvironment _environment;
    private readonly IFileSystem _fileSystem;
    private readonly IDateOnly _dateOnlyWrapper;
    private readonly IProcess _process;
    private readonly ILogger<DistributeCreciPayRequestHandler> _logger;
    private readonly ICreciPayHttpClient _creciPayHttpClient;
    private readonly IOptions<StoreSettings> _storeOptions;

    public DistributeCreciPayRequestHandler(IEnvironment environment, IFileSystem fileSystem, IDateOnly dateOnlyWrapper,
        IProcess process, ILogger<DistributeCreciPayRequestHandler> logger, ICreciPayHttpClient creciPayHttpClient,
        IOptions<StoreSettings> storeOptions)
    {
        _environment = environment;
        _fileSystem = fileSystem;
        _dateOnlyWrapper = dateOnlyWrapper;
        _process = process;
        _logger = logger;
        _creciPayHttpClient = creciPayHttpClient;
        _storeOptions = storeOptions;
    }

    public async Task Handle(DistributeCreciPayRequest request, CancellationToken cancellationToken)
    {
        if (request.Final)
        {
            await DistributeCreciPayFinal();
        }
        else
        {
            await DistributeCreciPayRecurring();
        }
    }

    private async Task DistributeCreciPayFinal()
    {
        var iberPath = _environment.GetEnvironmentVariable(IBERDIR) ??
                       throw new InvalidOperationException("IBERDIR not set");
        var businessDate = _dateOnlyWrapper.FromDateTime(DateTime.Today).AddDays(-1);
        var businessDatePath = _fileSystem.Path.Combine(iberPath, businessDate.ToString("yyyyMMdd"));
        if (_fileSystem.Directory.Exists(businessDatePath))
        {
            var files = _fileSystem.Directory.GetFiles(businessDatePath);

            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var fileName = _fileSystem.Path.GetFileName(file)?.ToLower();

                    if (fileName == null) continue;

                    if (_storeOptions.Value.Filenames != null &&
                        !_storeOptions.Value.Filenames.Select(x => x.ToLower()).Contains(fileName)) continue;

                    _logger.LogInformation("Adding {FileName} to zip", fileName);
                    var entry = archive.CreateEntry(Path.GetFileName(file));
                    await using var entryStream = entry.Open();
                    await using var fileStream = _fileSystem.File.OpenRead(file);
                    await fileStream.CopyToAsync(entryStream);
                }
            }

            memoryStream.Seek(0, SeekOrigin.Begin);
            await _creciPayHttpClient.UploadFinal(_storeOptions.Value.StoreId, _storeOptions.Value.Token, memoryStream);
        }

        Console.WriteLine("Distributing CreciPay Final for {0}", businessDate);
    }

    private async Task DistributeCreciPayRecurring()
    {
        _logger.LogInformation("Distributing CreciPay Recurring...");

        var iberPath = _environment.GetEnvironmentVariable(IBERDIR) ??
                       throw new InvalidOperationException("IBERDIR not set");
        var dataPath = _fileSystem.Path.Combine(iberPath, "Data");

        await GrindToday();

        var files = _fileSystem.Directory.GetFiles(dataPath);

        using var memoryStream = new MemoryStream();
        using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        {
            foreach (var file in files)
            {
                var fileName = _fileSystem.Path.GetFileName(file)?.ToLower();

                if (fileName == null) continue;

                if (_storeOptions.Value.Filenames != null &&
                    !_storeOptions.Value.Filenames.Select(x => x.ToLower()).Contains(fileName)) continue;

                _logger.LogInformation("Adding {FileName} to zip", fileName);
                var entry = archive.CreateEntry(Path.GetFileName(file));
                await using var entryStream = entry.Open();
                await using var fileStream = _fileSystem.File.OpenRead(file);
                await fileStream.CopyToAsync(entryStream);
            }
        }

        memoryStream.Seek(0, SeekOrigin.Begin);
        await _creciPayHttpClient.UploadRecurring(_storeOptions.Value.StoreId, _storeOptions.Value.Token, memoryStream);
    }

    private Task GrindToday()
    {
        var iberPath = _environment.GetEnvironmentVariable(IBERDIR) ??
                       throw new InvalidOperationException("IBERDIR not set");
        var binPath = _fileSystem.Path.Combine(iberPath, "bin");

        var processStartInfo = new ProcessStartInfo
        {
            FileName = "grind",
            Arguments = "/TODAY",
            WorkingDirectory = binPath,
        };

        _logger.LogInformation("Starting Grind for Today");
        var process = _process.Start(processStartInfo);
        return process?.WaitForExitAsync() ?? Task.CompletedTask;
    }
}