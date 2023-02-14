using System;
using UnityEngine;

namespace Diabloid
{
    public class EnemyAnimator : MonoBehaviour, IAnimationStateReader
    {
        private static readonly int DieHash = Animator.StringToHash("Die");
        private static readonly int IsMovingHash = Animator.StringToHash("IsMoving");
        private static readonly int AttackHash = Animator.StringToHash("Attack1");
        private static readonly int HitHash = Animator.StringToHash("Hit");

        private readonly int _idleStateHash = Animator.StringToHash("idle");
        private readonly int _moveStateHash = Animator.StringToHash("move");
        private readonly int _dieStateHash = Animator.StringToHash("die");
        private readonly int _attackStateHash = Animator.StringToHash("attack1");
        private readonly int _ðøåStateHash = Animator.StringToHash("GetHit");

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
            _animator.SetTrigger(DieHash);

        public void Move() =>
            _animator.SetBool(IsMovingHash, true);

        public void Stop() =>
            _animator.SetBool(IsMovingHash, false);

        public void PlayAttack() =>
            _animator.SetBool(AttackHash, true);

        public void StopAttack() =>
            _animator.SetBool(AttackHash, false);

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
