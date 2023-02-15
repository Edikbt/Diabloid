using UnityEngine;
using UnityEngine.AI;

namespace Diabloid
{
    public class AnimateAlongMove : MonoBehaviour
    {        
        [SerializeField] private NavMeshAgent _agent;
        
        private IMoveAnimate _animator;

        private const float MinVelocity = 0.1f;

        private void Awake()
        {
            _animator = GetComponent<IMoveAnimate>();
        }

        private void Update()
        {
            if (ShouldMove == true)
                _animator.Move();
            else
                _animator.StopMove();
        }

        private bool ShouldMove => 
            _agent.velocity.magnitude > MinVelocity && _agent.remainingDistance > _agent.radius;
    }
}
