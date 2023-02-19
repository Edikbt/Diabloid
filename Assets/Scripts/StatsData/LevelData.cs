using System.Collections.Generic;
using UnityEngine;

namespace Diabloid
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StatsData/Level")]
    public class LevelData : ScriptableObject
    {
        public string LevelName;

        public List<EnemySpawnerData> EnemySpawners;
    }
}