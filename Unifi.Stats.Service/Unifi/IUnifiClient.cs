using System.Collections.Generic;
using System.Threading.Tasks;
using Unifi.Stats.Service.Unifi.Model;

namespace Unifi.Stats.Service.Unifi
{
    public interface IUnifiClient
    {
        Task<IEnumerable<UnifiNetworkDevice>> GetDevicesAsync();
        Task<IEnumerable<UnifiNetworkClient>> GetClientsAsync();
    }
}