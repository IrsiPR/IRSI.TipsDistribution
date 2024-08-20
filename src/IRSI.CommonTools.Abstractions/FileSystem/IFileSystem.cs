namespace IRSI.CommonTools.Abstractions.FileSystem;

public interface IFileSystem
{
    IFile File { get; }
    IPath Path { get; }
    IDirectory Directory { get; }
    IFileStream FileStream { get; }
}