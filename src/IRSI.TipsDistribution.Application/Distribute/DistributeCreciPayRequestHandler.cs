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
    private readonly IEnvironment _environment;
    private readonly IFileSystem _fileSystem;
    private readonly IDateOnly _dateOnly;
    private readonly IProcess _process;
    private readonly ILogger<DistributeCreciPayRequestHandler> _logger;
    private readonly ICreciPayHttpClient _creciPayHttpClient;
    private readonly IOptions<StoreSettings> _storeOptions;
    private readonly IOptions<CreciPaySettings> _creciPayOptions;

    public DistributeCreciPayRequestHandler(IEnvironment environment,
        IFileSystem fileSystem,
        IDateOnly dateOnly,
        IProcess process,
        ILogger<DistributeCreciPayRequestHandler> logger,
        ICreciPayHttpClient creciPayHttpClient,
        IOptions<StoreSettings> storeOptions, IOptions<CreciPaySettings> creciPayOptions)
    {
        _environment = environment;
        _fileSystem = fileSystem;
        _dateOnly = dateOnly;
        _process = process;
        _logger = logger;
        _creciPayHttpClient = creciPayHttpClient;
        _storeOptions = storeOptions;
        _creciPayOptions = creciPayOptions;
    }

    private const string IBERDIR = "IBERDIR";

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
        var businessDate = _dateOnly.FromDateTime(DateTime.Today).AddDays(-1);
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
            var response = await _creciPayHttpClient.UploadFinal(_storeOptions.Value.StoreId,
                _creciPayOptions.Value.Token, memoryStream);
            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("CreciPay Recurring uploaded successfully");
            }
            else
            {
                _logger.LogInformation("Failed to upload CreciPay Recurring with {StatusCode}", response.StatusCode);
                var content = await response.Content.ReadAsStringAsync();
                _logger.LogError("Response content: {Content}", content);
            }
        }
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
        var response = await _creciPayHttpClient.UploadRecurring(_storeOptions.Value.StoreId,
            _creciPayOptions.Value.Token, memoryStream);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation("CreciPay Recurring uploaded successfully");
        }
        else
        {
            _logger.LogInformation("Failed to upload CreciPay Recurring with {StatusCode}", response.StatusCode);
            var content = await response.Content.ReadAsStringAsync();
            _logger.LogError("Response content: {Content}", content);
        }
    }

    private Task GrindToday()
    {
        var iberPath = _environment.GetEnvironmentVariable(IBERDIR) ??
                       throw new InvalidOperationException("IBERDIR not set");
        var binPath = _fileSystem.Path.Combine(iberPath, "bin");
        var exePath = _fileSystem.Path.Combine(binPath, "grind.exe");

        var processStartInfo = new ProcessStartInfo
        {
            FileName = exePath,
            Arguments = "/TODAY",
            WorkingDirectory = binPath,
        };

        _logger.LogInformation("Starting Grind for Today");
        var process1 = _process.Start(processStartInfo);
        return process1?.WaitForExitAsync() ?? Task.CompletedTask;
    }
}