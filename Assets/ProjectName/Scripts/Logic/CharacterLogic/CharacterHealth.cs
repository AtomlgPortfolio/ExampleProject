using System;
using ProjectName.Scripts.Data;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using ProjectName.Scripts.Utilities;
using UnityEngine;

namespace ProjectName.Scripts.Logic.CharacterLogic
{
    public class CharacterHealth : MonoBehaviour, ISavedProgress, IHealth, IDisposable
    {
        public event Action Changed;

        public float Value
        {
            get => _state.CurrentHp;
            set
            {
                if (Math.Abs(_state.CurrentHp - value) > Constants.Epsilon)
                {
                    _state.CurrentHp = value;
                    Changed?.Invoke();
                }
            }
        }

        public float MaxValue
        {
            get => _state.MaxHp;
            set => _state.MaxHp = value;
        }

        private CharacterAnimator _characterAnimator;
        private State _state;
        private bool _isDisposed;

        public void Initialize(CharacterAnimator characterAnimator)
        {
            ExceptionValidator.ThrowIfNull(characterAnimator);
            _characterAnimator = characterAnimator;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            _state = progress.HeroState;
            ExceptionValidator.ThrowIfNull(_state);
            Changed?.Invoke();
        }

        public void UpdateProgress(PlayerProgress progress) => progress.HeroState = _state;

        public void TakeDamage(float damage)
        {
            ExceptionValidator.ThrowIfTrue(damage < 0);
            if (Value <= 0)
                return;

            Value -= damage;
            _characterAnimator.PlayHit();
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
                _characterAnimator = null;
                _state = null;
            }

            _isDisposed = true;
        }

        ~CharacterHealth()
        {
            Dispose(disposing: false);
        }
    }
}