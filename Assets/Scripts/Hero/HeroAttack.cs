using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroAttack : Attack, ISavedProgressReader
    {
        private void Awake()
        {
            _animator = GetComponent<IAttackAnimate>();
            _layerMask = 1 << LayerMask.NameToLayer("EnemyHittable");
        }

        public void LoadProgress(PlayerProgress progress)
        {
            Damage = progress.HeroStats.Damage;
            Radius = progress.HeroStats.DamageRadius;
        }
    }
}
