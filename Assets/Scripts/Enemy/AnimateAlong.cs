using UnityEngine;
using UnityEngine.AI;

namespace Diabloid
{
    public class AnimateAlong : MonoBehaviour
    {        
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private EnemyAnimator _animator;

        private const float MinVelocity = 0.1f;

        private void Update()
        {
            if (ShouldMove == true)
                _animator.Move();
            else
                _animator.Stop();
        }

        private bool ShouldMove => 
            _agent.velocity.magnitude > MinVelocity && _agent.remainingDistance > _agent.radius;
    }
}
