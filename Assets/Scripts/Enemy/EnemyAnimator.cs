using System;
using UnityEngine;

namespace Diabloid
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int Die = Animator.StringToHash("Die");
        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        private static readonly int Attack = Animator.StringToHash("Attack1");

        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _moveStateHash = Animator.StringToHash("move");
        private readonly int _dieStateHash = Animator.StringToHash("die");
        private readonly int _attackStateHash = Animator.StringToHash("attack1");

        [SerializeField] Animator _animator;

        public AnimatorState State { get; private set; }

        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            State = StateFor(stateHash);
            StateExited?.Invoke(State);
        }

        public void PlayDeath() =>
            _animator.SetTrigger(Die);

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;

            if (stateHash == _idleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _moveStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _dieStateHash)
                state = AnimatorState.Died;
            else if (stateHash == _attackStateHash)
                state = AnimatorState.Attack;
            else
                state = AnimatorState.Unknown;

            return state;
        }
    }
}
