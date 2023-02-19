using UnityEngine;
using Zenject;

namespace Diabloid
{
    public class HeroChooseEnemy : MonoBehaviour
    {
        [SerializeField] private MoveTo _moveTo;
        private IInputService _inputService;

        [Inject]
        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _inputService.OnEnemyTouched += SetEnemy;
        }

        private void OnDestroy()
        {
            _inputService.OnEnemyTouched -= SetEnemy;
        }

        private void SetEnemy(GameObject enemy) => 
            _moveTo.SetTarget(enemy.transform);
    }
}
