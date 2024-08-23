using IRSI.CommonTools.Abstractions.FileSystem;

namespace IRSI.CommonTools.FileSystem;

public class DirectoryWrapper : IDirectory
{
    public string GetCurrentDirectory() => Directory.GetCurrentDirectory();
    public DirectoryInfo CreateDirectory(string path) => Directory.CreateDirectory(path);
    public bool Exists(string path) => Directory.Exists(path);
    public string[] GetDirectories(string path, string searchPattern) => Directory.GetDirectories(path, searchPattern);
    public IEnumerable<string> EnumerateDirectories(string path, string searchPattern) => Directory.EnumerateDirectories(path, searchPattern);
    public string[] GetFiles(string path) => Directory.GetFiles(path);
}