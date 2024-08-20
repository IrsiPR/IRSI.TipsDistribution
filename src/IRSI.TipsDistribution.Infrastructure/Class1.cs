using IRSI.CommonTools.Abstractions;
using IRSI.CommonTools.Abstractions.FileSystem;
using IRSI.TipsDistribution.Application.Contracts;

namespace IRSI.TipsDistribution.Infrastructure;

public class DatedFoldersService(IEnvironment environment, IFileSystem fileSystem) : IDatedFoldersService
{
    private const string IBERDIR = "IBERDIR";

    public IEnumerable<string> GetDatedFolders()
    {
        var iberdir = environment.GetEnvironmentVariable(IBERDIR);
        return fileSystem.Directory.EnumerateDirectories(iberdir!, "20??????");
    }

    public string GetFullPath(string datePortion)
    {
        var iberdir = environment.GetEnvironmentVariable(IBERDIR);
        return fileSystem.Path.Combine(iberdir, datePortion);
    }
}