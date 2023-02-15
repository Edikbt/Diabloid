using System.Collections;
using UnityEngine;

namespace Diabloid
{
    public class EnemyAggro : MonoBehaviour
    {
        [SerializeField] private TriggerObserver _triggerObserver;
        [SerializeField] private MoveTo _followToPlayer;
        [SerializeField] private float _cooldown;

        private Coroutine _aggroCoroutine;
        private bool _hasAggroTarget;

        private void Start()
        {
            _triggerObserver.TriggerEnter += TriggerEnter;
            _triggerObserver.TriggerExit += TriggerExit;

            _followToPlayer.enabled = false;
        }

        private void TriggerEnter(Collider collider)
        {
            if (_hasAggroTarget == false)
            {
                _hasAggroTarget = true;
                StopAggroCoroutine();
            }

            _followToPlayer.enabled = true;
        }

        private void StopAggroCoroutine()
        {
            if (_aggroCoroutine != null)
            {
                StopCoroutine(_aggroCoroutine);
                _aggroCoroutine = null;
            }
        }

        private void TriggerExit(Collider collider)
        {
            if (_hasAggroTarget == true)
            {
                _hasAggroTarget = false;
                _aggroCoroutine = StartCoroutine(SwitchFollowOffAfterCooldown());
            }
        }

        private IEnumerator SwitchFollowOffAfterCooldown()
        {
            yield return new WaitForSeconds(_cooldown);
            _followToPlayer.enabled = false;
        }
    }
}
