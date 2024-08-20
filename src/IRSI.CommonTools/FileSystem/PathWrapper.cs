using IRSI.CommonTools.Abstractions.FileSystem;

namespace IRSI.CommonTools.FileSystem;

public class PathWrapper : IPath
{
    public string Combine(params string[] paths) => Path.Combine(paths);

    public ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path) => Path.GetFileName(path);
}