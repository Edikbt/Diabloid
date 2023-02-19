using System.Collections.Generic;
using UnityEngine;

namespace Diabloid
{
    public class GameFactory : IGameFactory
    {
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();
        public GameObject Hero { get; private set; }

        private readonly IAssetProvider _assetProvider;
        private readonly IStatsDataService _statsDataService;        

        public GameFactory(IAssetProvider assetProvider, IStatsDataService statsDataService)
        {
            _assetProvider = assetProvider;
            _statsDataService = statsDataService;
        }

        public GameObject CreateHero()
        {
            Hero = InstantiateRegistered(AssetAddress.HeroPath);
            return Hero;
        }

        public GameObject CreateEnemy(EnemyTypeId enemyTypeId, Transform parent)
        {
            EnemyStatsData enemyData = _statsDataService.Enemy(enemyTypeId);
            GameObject enemy = _assetProvider.Instantiate(enemyData, parent);

            enemy.GetComponent<MoveTo>().SetTarget(Hero.transform);

            IHealth health = enemy.GetComponent<IHealth>();
            health.Current = enemyData.HP;
            health.Max = enemyData.HP;

            EnemyAttack attack = enemy.GetComponent<EnemyAttack>();
            attack.SetTarget(Hero.transform);
            attack.Damage = enemyData.Damage;
            attack.Radius = enemyData.Radius;
            attack.EffectiveDistance = enemyData.EffectiveDistance;

            EnemyLoot loot = enemy.GetComponentInChildren<EnemyLoot>();
            loot.SetGold(enemyData.MinGold, enemyData.MaxGold);

            return enemy;
        }

        public GameObject CreateHud() =>
            InstantiateRegistered(AssetAddress.HUDPath);

        public LootGold CreateLoot() => 
            InstantiateRegistered(AssetAddress.LootChestPath).GetComponent<LootGold>();

        public void CreateSpawner(Vector3 position, string spawnerId, EnemyTypeId enemyTypeId)
        {
            SpawnPoint spawner = InstantiateRegistered(AssetAddress.Spawner).GetComponent<SpawnPoint>();
            spawner.transform.position = position;
            spawner.Id = spawnerId;
            spawner.EnemyTypeId = enemyTypeId;
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
                ProgressWriters.Add(progressWriter);

            ProgressReaders.Add(progressReader);
        }

        private GameObject InstantiateRegistered(string prefabPath)
        {
            GameObject gameObject = _assetProvider.Instantiate(prefabPath);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject hero)
        {
            foreach (ISavedProgressReader progressReader in hero.GetComponentsInChildren<ISavedProgressReader>())
                Register(progressReader);
        }        
    }
}
