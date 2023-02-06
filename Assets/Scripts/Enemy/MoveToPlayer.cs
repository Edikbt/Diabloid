using UnityEngine;
using UnityEngine.AI;

namespace Diabloid
{
    public class MoveToPlayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;

        private const float MinimalDistance = 1;

        private Transform _heroTransform;

        public void Construct(Transform heroTransform) =>
          _heroTransform = heroTransform;

        private void Update()
        {
            if (_heroTransform && IsHeroNotReached())
                _agent.destination = _heroTransform.position;
        }

        //private bool IsHeroNotReached() => _agent.transform.position.SqrMagnitudeTo(_heroTransform.position) >= MinimalDistance;
        
        private bool IsHeroNotReached() => 
            Vector3.Distance(_agent.transform.position, _heroTransform.position) >= MinimalDistance;

    }
}
