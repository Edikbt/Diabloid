using UnityEngine;
using UnityEngine.AI;

namespace Diabloid
{
    public class MoveTo : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private const float MinimalDistance = 1;

        private Transform _target;

        public void SetTaret(Transform target) =>
          _target = target;

        private void Update()
        {
            if (_target && IsTargetNotReached())
                _agent.destination = _target.position;
        }

        //private bool IsHeroNotReached() => _agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
        
        private bool IsTargetNotReached() => 
            Vector3.Distance(_agent.transform.position, _target.position) >= MinimalDistance;

    }
}
