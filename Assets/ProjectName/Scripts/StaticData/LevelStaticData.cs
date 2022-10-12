using UnityEngine;

namespace ProjectName.Scripts.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Static Data/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public Vector3 InitialHeroPosition;
    }
}