using System;
using System.Collections;
using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(EnemyAnimator), typeof(EnemyHealth))]
    public class EnemyDeath : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private EnemyAnimator _animator;

        public event Action EnemyDied;

        private void Start()
        {
            _health.HealthChanged += HealthChanged;
        }

        private void OnDestroy()
        {
            _health.HealthChanged -= HealthChanged;
        }

        private void HealthChanged()
        {
            if (_health.Current <= 0)
                Die();
        }

        private void Die()
        {
            _health.HealthChanged -= HealthChanged;
            _animator.PlayDeath();

            StartCoroutine(ClearDead());
            EnemyDied?.Invoke();
        }

        private IEnumerator ClearDead()
        {
            yield return new WaitForSeconds(3);
            Destroy(gameObject);
        }
    }
}
