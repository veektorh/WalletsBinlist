using System.Threading.Tasks;

namespace BinlistApi.Interfaces.Managers
{
    public interface IBinlistManager
    {
        Task<Bin> GetBinDetails(string bin);
    }
}