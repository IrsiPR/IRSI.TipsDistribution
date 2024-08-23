using System.Collections;

namespace IRSI.CommonTools.Abstractions;

public interface IEnvironment
{
    string GetCommandLine { get; }
    string CurrentDirectory { get; }
    string? GetEnvironmentVariable(string variable);
    string? GetEnvironmentVariable(string variable, EnvironmentVariableTarget target);
    IDictionary GetEnvironmentVariables();
    IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target);
    void SetEnvironmentVariable(string variable, string value);
    void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target);
    string ExpandEnvironmentVariables(string name);
    void FailFast(string message);
    void FailFast(string message, Exception exception);
    string[] GetLogicalDrives();
    string? GetFolderPath(Environment.SpecialFolder folder);
    string? GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option);
    string[] GetCommandLineArgs();
}