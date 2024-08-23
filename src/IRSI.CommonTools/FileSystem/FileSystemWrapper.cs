using IRSI.CommonTools.Abstractions.FileSystem;

namespace IRSI.CommonTools.FileSystem;

public class FileSystemWrapper : IFileSystem
{
    private IFile? _fileService;
    private IPath? _pathService;
    private IDirectory? _directoryService;
    private IFileStream? _fileStream;

    public IFile File => _fileService ??= new FileWrapper();
    public IPath Path => _pathService ??= new PathWrapper();
    public IDirectory Directory => _directoryService ??= new DirectoryWrapper();
    public IFileStream FileStream => _fileStream ??= new FileStreamWrapper();
}