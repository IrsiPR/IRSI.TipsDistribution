namespace IRSI.TipsDistribution.Application.Contracts;

public interface IDatedFoldersService
{
    IEnumerable<string> GetDatedFolders();
    string GetFullPath(string datePortion);
}