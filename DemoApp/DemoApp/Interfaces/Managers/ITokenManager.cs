using System.Threading.Tasks;

namespace DemoApp.Interfaces.Managers
{
    public interface ITokenManager
    {
        Task<string> GetToken();
        Task<string> RequestNewToken();
    }
}