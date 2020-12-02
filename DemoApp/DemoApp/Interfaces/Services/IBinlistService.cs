using System.Threading.Tasks;
using DemoApp.Models;

namespace DemoApp.Interfaces.Services
{
    public interface IBinlistService
    {
        Task<Bin> GetBinDetails(string bin, int count = 0);
    }
}