using System.Threading.Tasks;

namespace BinlistApi.Interfaces.Services
{
    public interface IBinlistService
    {
        Task<Bin> GetBinDetails(string bin);
    }
}