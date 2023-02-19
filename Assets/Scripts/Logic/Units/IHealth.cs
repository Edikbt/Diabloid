using System;

namespace Diabloid
{
    public interface IHealth
    {
        float Current { get; set; }
        float Max { get; set; }

        event Action HealthChanged;

        bool TakeDamage(float damage);
    }
}