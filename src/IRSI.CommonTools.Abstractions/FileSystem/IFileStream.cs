namespace IRSI.CommonTools.Abstractions.FileSystem;

public interface IFileStream
{
    FileStream Create(string path, FileMode fileMode);
}