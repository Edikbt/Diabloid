using System;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Diabloid
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class Attack : MonoBehaviour
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _attackCooldown;
        [SerializeField] private float _cleavage;
        [SerializeField] private float _effectiveDistance;

        private Transform _heroTransform;
        private float _currentCooldown = 0;

        private bool _isAttacking = false;
        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private bool _attackIsActive;

        [Inject]
        private void Construct(IGameFactory gameFactory) => 
            _heroTransform = gameFactory.Hero.transform;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Player");
        }

        private void Update()
        {
            if (_currentCooldown > 0)
                _currentCooldown -= Time.deltaTime;

            if (_attackIsActive == true && _isAttacking == false && _currentCooldown <= 0)
                StartAttack();

            //if (Input.GetKeyUp(KeyCode.T))
            //    StartAttack();
        }

        private void OnAttack()
        {
            if (Hit(out Collider hit))
                PhysicsDebug.DrawDebug(StartPoint(), _cleavage, 1);
        }

        private void OnAttackEnded()
        {
            _currentCooldown = _attackCooldown;
            _isAttacking = false;
            _animator.StopAttack();
        }

        public void Enable() => 
            _attackIsActive = true;

        public void Disable() => 
            _attackIsActive = false;

        private bool Hit(out Collider hit)
        {
            int size = Physics.OverlapSphereNonAlloc(StartPoint(), _cleavage, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return size > 0;
        }

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z) + transform.forward * _effectiveDistance;

        private void StartAttack()
        {
            transform.LookAt(_heroTransform);
            _isAttacking = true;
            _animator.PlayAttack();
        }
    }
}
