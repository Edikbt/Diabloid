using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(Attack))]
    public class CheckAttackRange : MonoBehaviour
    {
        [SerializeField] private Attack _attack;
        [SerializeField] private TriggerObserver _triggerObserver;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter; 
            _triggerObserver.TriggerExit += TriggerExit;

            _attack.Disable();
        }

        private void TriggerEnter(Collider obj)
        {
            //Debug.Log($"TriggerEnter Hero Attack zone {gameObject.name} {LayerMask.LayerToName(obj.gameObject.layer)}");

            _attack.Enable();
        }

        private void TriggerExit(Collider obj) => 
            _attack.Disable();

    }
}
