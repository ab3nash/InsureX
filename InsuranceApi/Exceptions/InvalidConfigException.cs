namespace InsuranceApi.Exceptions;

public class InvalidConfigException : Exception
{
    public string ConfigName { get; }

    public InvalidConfigException(string configName): base($"Config {configName} is missing or misconfigured")
    {
        ConfigName = configName;
    }
}