using System.Collections.Generic;
using System.Linq;
using ProjectName.Scripts.StaticData;
using UnityEngine;

namespace ProjectName.Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string StaticDataLevelsPath = "StaticData/Levels";

        private Dictionary<string, LevelStaticData> _levels;

        public void LoadData()
        {
            _levels = Resources
                .LoadAll<LevelStaticData>(StaticDataLevelsPath)
                .ToDictionary(x => x.LevelKey, x => x);
        }


        public LevelStaticData ForLevel(string sceneKey) =>
            _levels.TryGetValue(sceneKey, out LevelStaticData levelStaticData)
                ? levelStaticData
                : null;
    }
}