namespace lproject.HostConfiguration.ConfigurationAccessor;

public interface IConfigurationAccessor
{
    T GetValue<T>(string key);
}