using UnityEngine;
using UnityEngine.AI;

namespace Diabloid
{
    public class MoveTo : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private const float MinimalDistance = 1;

        private Transform _target;        

        private void Update()
        {
            if (_target && IsTargetNotReached())
                _agent.destination = _target.position;
        }

        public void SetTarget(Transform target) =>
          _target = target;

        public void ClearTarget() =>
          _target = null;

        private bool IsTargetNotReached() => 
            Vector3.Distance(_agent.transform.position, _target.position) >= MinimalDistance;

    }
}
