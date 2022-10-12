using ProjectName.Scripts.StaticData;

namespace ProjectName.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        void LoadData();
        LevelStaticData ForLevel(string sceneKey);
    }
}