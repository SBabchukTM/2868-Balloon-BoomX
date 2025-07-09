using UnityEngine;

namespace Runtime.Gameplay.HelperServices.NetworkConnection
{
    public class NetworkConnectionService : INetworkConnectionService
    {
        bool INetworkConnectionService.IsInternetReachable()
        {
            return UnityEngine.Application.internetReachability != NetworkReachability.NotReachable;
        }
    }
}