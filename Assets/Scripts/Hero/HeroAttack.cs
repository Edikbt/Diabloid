using System.Linq;
using UnityEngine;

namespace Diabloid
{
    [RequireComponent(typeof(HeroAnimator))]
    public class HeroAttack : MonoBehaviour, ISavedProgressReader
    {
        [SerializeField] private HeroAnimator _animator;        

        private int _layerMask;
        private Collider[] _hits = new Collider[1];
        private Stats _stats;

        private void Awake()
        {
            _layerMask = 1 << LayerMask.NameToLayer("Hittable");
        }

        private void OnAttack()
        { 
            if (Hit(out Collider hit))
                hit.transform.parent.GetComponent<IHealth>().TakeDamage(_stats.Damage);
        }

        private void OnAttackEnded()
        { 

        }

        private bool Hit(out Collider hit)
        {
            int size = Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();

            return size > 0;
        }

        private Vector3 StartPoint()
        {
            return transform.position;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
        }
    }
}
