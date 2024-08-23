namespace IRSI.CommonTools.Abstractions.FileSystem;

public interface IPath
{
    string Combine(params string[] paths);
    ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path);
    string? GetFileName(string? path);
}