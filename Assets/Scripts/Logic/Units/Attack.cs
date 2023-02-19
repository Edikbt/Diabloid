using System.Linq;
using UnityEngine;

namespace Diabloid
{
    public class Attack : MonoBehaviour
    {
        public float AttackCooldown;
        public float Radius;
        public float EffectiveDistance;
        public float Damage;

        protected IAttackAnimate _animator;
        protected Transform _target;
        protected int _layerMask;

        protected Collider[] _hits = new Collider[1];
        protected bool _attackIsActive;
        protected float _currentCooldown = 0;
        protected bool _isAttacking = false;

        private void Update()
        {
            if (_currentCooldown > 0)
                _currentCooldown -= Time.deltaTime;

            if (_attackIsActive == true && _isAttacking == false && _currentCooldown <= 0)
                StartAttack();
        }

        private void OnAttack()
        {
            //Debug.Log($"{gameObject.name} OnAttack");

            if (Hit(out Collider hit))
            {
                //Debug.Log($"Hit {hit.gameObject.name}");
                hit.GetComponentInParent<IHealth>().TakeDamage(Damage);
            }
        }

        private void OnAttackEnded()
        {
            _currentCooldown = AttackCooldown;
            _isAttacking = false;
            _animator.StopAttack();
        }

        public void SetTarget(Transform target) =>
            _target = target;

        public void Enable() => 
            _attackIsActive = true;

        public void Disable() => 
            _attackIsActive = false;

        protected void StartAttack()
        {
            transform.LookAt(_target);
            _isAttacking = true;
            _animator.PlayAttack();
        }

        protected bool Hit(out Collider hit)
        {
            int size = Physics.OverlapSphereNonAlloc(StartPoint(), Radius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return size > 0;
        }

        protected virtual Vector3 StartPoint() => 
            transform.position;
    }
}
