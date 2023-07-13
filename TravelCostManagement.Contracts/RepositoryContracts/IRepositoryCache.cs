using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCostManagement.Contracts.RepositoryContracts
{
    public interface IRepositoryCache
    {
        List<T> GetCache<T>(string userName);

        void SetCache<T>(string key, List<T> generic);

        T GetCacheObject<T>(string userName, T defaultResponse);

        void SetCacheObject<T>(string key, T generic);
    }
}
