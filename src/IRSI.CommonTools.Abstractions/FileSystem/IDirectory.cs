namespace IRSI.CommonTools.Abstractions.FileSystem;

public interface IDirectory
{
    string GetCurrentDirectory();
    DirectoryInfo CreateDirectory(string path);
    bool Exists(string path);
    string[] GetDirectories(string path, string searchPattern);
    string[] GetFiles(string path);
    IEnumerable<string> EnumerateDirectories(string path, string searchPattern);
}