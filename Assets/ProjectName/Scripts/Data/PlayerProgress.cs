using System;

namespace ProjectName.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public State HeroState;
        public Stats HeroStats;
        public WorldData WorldData;

        public PlayerProgress(string initialLevelName)
        {
            WorldData = new WorldData(initialLevelName);
            HeroState = new State();
            HeroStats = new Stats();
        }
    }
}