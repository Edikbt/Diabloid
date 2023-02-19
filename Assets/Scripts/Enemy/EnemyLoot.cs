using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class EnemyLoot : MonoBehaviour
    {
        [SerializeField] private EnemyDeath _enemyDeath;
        private IGameFactory _gameFactory;
        private int _goldMin;
        private int _goldMax;

        [Inject]
        private void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        private void Start()
        {
            _enemyDeath.EnemyDied += SpawnLoot;
        }

        public void SetGold(int min, int max)
        {
            _goldMin = min;
            _goldMax = max;
        }

        private void SpawnLoot()
        {
            LootGold loot = _gameFactory.CreateLoot();
            loot.transform.position = transform.position;

            Gold gold = new Gold { Value = Random.Range(_goldMin, _goldMax) };
            loot.Initialize(gold);
        }        
    }
}
