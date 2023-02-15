using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyCheckAttackRange : MonoBehaviour
    {
        [SerializeField] private EnemyAttack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter; 
            _triggerObserver.TriggerExit += TriggerExit;

            _attack.Disable();
        }

        private void TriggerEnter(Collider obj) => 
            _attack.Enable();

        private void TriggerExit(Collider obj) => 
            _attack.Disable();

    }
}
