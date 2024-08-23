using IRSI.CommonTools.Abstractions.FileSystem;

namespace IRSI.CommonTools.FileSystem;

public class FileStreamWrapper : IFileStream
{
    public FileStream Create(string path, FileMode fileMode) => new(path, fileMode);
}