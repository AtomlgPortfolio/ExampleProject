using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectName.Scripts.Infrastructure.Services;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace ProjectName.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        Task<GameObject> CreateHero(Vector3 initialPointPosition);
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        void CleanUp();
        void Register(ISavedProgressReader progressReader);
        void Unregister(ISavedProgressReader progressReader);
    }
}