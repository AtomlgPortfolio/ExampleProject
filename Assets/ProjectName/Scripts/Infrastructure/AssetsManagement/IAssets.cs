using System.Threading.Tasks;
using ProjectName.Scripts.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace ProjectName.Scripts.Infrastructure.AssetsManagement
{
    public interface IAssets : IService
    {
        Task<GameObject> InstantiateAsync(string address);
        Task<GameObject> InstantiateAsync(string address, Vector3 at);
        Task<GameObject> InstantiateAsync(string address, Transform under);
        Task<T> Load<T>(AssetReference assetReference) where T : class;
        Task<T> Load<T>(string address) where T : class;
        void CleanUp();
    }
}