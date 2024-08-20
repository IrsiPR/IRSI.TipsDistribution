namespace IRSI.CommonTools.Abstractions.FileSystem;

public interface IFile
{
    FileStream OpenRead(string path);
    FileStream Create(string path);
    bool Exists(string path);
    void Delete(string path);
}