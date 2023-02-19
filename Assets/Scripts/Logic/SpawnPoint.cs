using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class SpawnPoint : MonoBehaviour, ISavedProgress
    {
        public string Id { get; set; }
        public EnemyTypeId EnemyTypeId;
        public bool IsDead;
        
        private IGameFactory _gameFactory;
        private EnemyDeath _enemyDeath;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.EnemyDeathData.ClearedSpawners.Contains(Id))
                IsDead = true;
            else
                Spawn();
        }        

        public void UpdateProgress(PlayerProgress progress)
        {
            if (IsDead)
                progress.EnemyDeathData.ClearedSpawners.Add(Id);
        }

        private void Spawn()
        {
            GameObject enemy = _gameFactory.CreateEnemy(EnemyTypeId, transform);
            _enemyDeath = enemy.GetComponent<EnemyDeath>();
            _enemyDeath.EnemyDied += Death;
        }

        private void Death()
        {
            if (_enemyDeath != null)
                _enemyDeath.EnemyDied -= Death;

            IsDead = true;
        }
    }
}
