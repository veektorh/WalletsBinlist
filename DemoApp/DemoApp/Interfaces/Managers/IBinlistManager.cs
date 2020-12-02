using System.Collections.Generic;
using System.Threading.Tasks;
using DemoApp.Models;

namespace DemoApp.Interfaces.Managers
{
    public interface IBinlistManager
    {
        Task<Bin> GetBinDetails(string bin);
        Task<BinCountModel> GetCount();
        Task<long> IncrementCount(string bin);
    }
}