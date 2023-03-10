using System;
using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private float _current;
        [SerializeField] private float _max;

        public float Current
        {
            get => _current;
            set => _current = value;
        }

        public float Max
        {
            get => _max;
            set => _max = value;
        }

        public event Action HealthChanged;

        public bool TakeDamage(float damage)
        {
            Current -= damage;
            HealthChanged?.Invoke();

            return Current > 0;
        }
    }
}
