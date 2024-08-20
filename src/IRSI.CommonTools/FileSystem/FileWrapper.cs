using IRSI.CommonTools.Abstractions.FileSystem;

namespace IRSI.CommonTools.FileSystem;

public class FileWrapper : IFile
{
    public FileStream OpenRead(string path) => File.OpenRead(path);
    public FileStream Create(string path) => File.Create(path);
    public void Delete(string path) => File.Delete(path);
    public bool Exists(string? path) => File.Exists(path);
}