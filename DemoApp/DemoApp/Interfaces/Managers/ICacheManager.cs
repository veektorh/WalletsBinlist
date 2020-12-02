namespace DemoApp.Interfaces.Managers
{
    public interface ICacheManager
    {
        bool Exists(string key);
        string Get(string key);
        string Set(string key, string value);
    }
}