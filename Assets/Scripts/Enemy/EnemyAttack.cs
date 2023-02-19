using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(EnemyAttack))]
    public class EnemyAttack : Attack
    {
        private void Awake()
        {
            _animator = GetComponent<IAttackAnimate>();
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        protected override Vector3 StartPoint() => 
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * EffectiveDistance;
    }
}
