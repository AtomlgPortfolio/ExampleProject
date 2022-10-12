using System;
using ProjectName.Scripts.Utilities;
using UnityEngine;

namespace ProjectName.Scripts.Logic.CharacterLogic
{
    public class CharacterDeath : MonoBehaviour, IDisposable
    {
        private CharacterHealth _characterHealth;
        private CharacterMovement _characterMovement;
        private CharacterAnimator _characterAnimator;
        private CharacterAttack _characterAttack;

        private bool _isDead;
        private bool _isDisposed;

        public void Initialize(CharacterHealth characterHealth, CharacterMovement characterMovement, CharacterAnimator characterAnimator, CharacterAttack characterAttack)
        {
            ExceptionValidator.ThrowIfNull(characterHealth, characterMovement, characterAnimator, characterAttack);
            _characterHealth = characterHealth;
            _characterMovement = characterMovement;
            _characterAnimator = characterAnimator;
            _characterAttack = characterAttack;

            _characterHealth.Changed += OnChanged;
        }

        private void OnChanged()
        {
            if (!_isDead && _characterHealth.Value <= 0f)
                Die();
        }

        private void Die()
        {
            _isDead = true;
            _characterMovement.enabled = false;
            _characterAnimator.PlayDeath();
            _characterAttack.enabled = false;
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
                if (_characterHealth != null)
                {
                    _characterHealth.Changed -= OnChanged;
                    _characterHealth = null;
                }
            
                _characterAnimator = null;
                _characterAttack = null;
            }

            _isDisposed = true;
        }
        
        ~CharacterDeath()
        {
            Dispose(disposing: false);
        }
    }
}