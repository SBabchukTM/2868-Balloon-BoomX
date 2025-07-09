using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public interface IAssetProvider : ICustomInitializer
    {
        UniTask<T> Load<T>(string address) where T : class;
        UniTask<GameObject> Instantiate(string address);
    }
}