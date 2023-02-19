using System;
using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroHealth : MonoBehaviour, ISavedProgress, IHealth
    {
        [SerializeField] private HeroAnimator _animator;
        private State _state;

        public event Action HealthChanged;

        public float Current
        {
            get => _state.CurrentHp;
            set
            {
                if (_state.CurrentHp != value)
                {
                    _state.CurrentHp = value;
                    HealthChanged?.Invoke();
                }
            }
        }

        public float Max 
        {
            get => _state.MaxHp; 
            set => _state.MaxHp = value;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            HealthChanged?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.HeroState.CurrentHp = Current;
            progress.HeroState.MaxHp = Max;
        }

        public bool TakeDamage(float damage)
        {
            if (Current <= 0)
                return false;

            Current -= damage;
            return Current > 0;
        }
    }
}
