using System;
using ProjectName.Scripts.Logic.AnimatorLogic;
using ProjectName.Scripts.Utilities;
using UnityEngine;

namespace ProjectName.Scripts.Logic.CharacterLogic
{
    public class CharacterAnimator : MonoBehaviour, IAnimationStateReader, IDisposable
    {
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;

        private Animator _animator;
        private CharacterAnimatorHashes _characterAnimatorHashes;
        private bool _isDisposed;

        public void Initialize(Animator animator)
        {
            ExceptionValidator.ThrowIfNull(animator);
            _animator = animator;
            _characterAnimatorHashes = new CharacterAnimatorHashes();
        }
        
        public void PlayAttack() => _animator.SetTrigger(_characterAnimatorHashes.AttackHash);
        public void PlayHit() => _animator.SetTrigger(_characterAnimatorHashes.HitHash);
        public void PlayDeath() =>  _animator.SetTrigger(_characterAnimatorHashes.DieHash);

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash) =>
            StateExited?.Invoke(StateFor(stateHash));

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _characterAnimatorHashes.IdleStateHash)
                state = AnimatorState.Idle;
            else if (stateHash == _characterAnimatorHashes.AttackStateHash)
                state = AnimatorState.Attack;
            else if (stateHash == _characterAnimatorHashes.WalkingStateHash)
                state = AnimatorState.Walking;
            else if (stateHash == _characterAnimatorHashes.DeathStateHash)
                state = AnimatorState.Died;
            else
                state = AnimatorState.None;

            return state;
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_isDisposed)
                return;

            if (disposing)
            {
                _characterAnimatorHashes = null;
                _animator = null;
            }
            
            _isDisposed = true;
        }

        ~CharacterAnimator()
        {
            Dispose(disposing: false);
        }
    }
}