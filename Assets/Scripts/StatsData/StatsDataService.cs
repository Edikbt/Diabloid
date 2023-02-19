using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Zenject;

namespace Diabloid
{
    public class StatsDataService : IStatsDataService, IInitializable
    {
        private const string EnemyStatsDataPath = "StatsData/Enemies";
        private const string LevelStatsDataPath = "StatsData/Levels";

        private Dictionary<EnemyTypeId, EnemyStatsData> _enemies;
        private Dictionary<string, LevelData> _levels;

        public void Initialize()
        {
            Load();
        }

        public void Load()
        {
            _enemies = Resources.LoadAll<EnemyStatsData>(EnemyStatsDataPath).ToDictionary(x => x.EnemyTypeId, x => x);
            _levels = Resources.LoadAll<LevelData>(LevelStatsDataPath).ToDictionary(x => x.LevelName, x => x);
        }

        public EnemyStatsData Enemy(EnemyTypeId typeId) =>
            _enemies.TryGetValue(typeId, out EnemyStatsData enemyStatsData) ? enemyStatsData : null;

        public LevelData Level(string sceneName) =>
            _levels.TryGetValue(sceneName, out LevelData levelStatsData) ? levelStatsData : null;
    }
}
