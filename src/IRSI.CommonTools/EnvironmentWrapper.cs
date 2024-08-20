using System.Collections;
using IRSI.CommonTools.Abstractions;

namespace IRSI.CommonTools;

public class EnvironmentWrapper : IEnvironment
{
    public string GetCommandLine => Environment.CommandLine;
    public string CurrentDirectory => Environment.CurrentDirectory;
    public string? GetEnvironmentVariable(string variable) => Environment.GetEnvironmentVariable(variable);

    public string? GetEnvironmentVariable(string variable, EnvironmentVariableTarget target) =>
        Environment.GetEnvironmentVariable(variable, target);

    public IDictionary GetEnvironmentVariables() => Environment.GetEnvironmentVariables();

    public IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target) =>
        Environment.GetEnvironmentVariables(target);

    public void SetEnvironmentVariable(string variable, string value) =>
        Environment.SetEnvironmentVariable(variable, value);

    public void SetEnvironmentVariable(string variable, string value, EnvironmentVariableTarget target) =>
        Environment.SetEnvironmentVariable(variable, value, target);

    public string ExpandEnvironmentVariables(string name) => Environment.ExpandEnvironmentVariables(name);
    public void FailFast(string message) => Environment.FailFast(message);
    public void FailFast(string message, Exception exception) => Environment.FailFast(message, exception);
    public string[] GetLogicalDrives() => Environment.GetLogicalDrives();
    public string? GetFolderPath(Environment.SpecialFolder folder) => Environment.GetFolderPath(folder);

    public string? GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option) =>
        Environment.GetFolderPath(folder, option);

    public string[] GetCommandLineArgs() => Environment.GetCommandLineArgs();
}