using System;
using ProjectName.Scripts.Data;
using ProjectName.Scripts.Infrastructure.Services;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using ProjectName.Scripts.Services.Inputs;
using ProjectName.Scripts.Utilities;
using UnityEngine;

namespace ProjectName.Scripts.Logic.CharacterLogic
{
    public class CharacterAttack : MonoBehaviour, ISavedProgressReader, IDisposable
    {
        private const string HittableLayerMaskName = "Hittable";
        private const int HitTargetsCount = 3;
        private const int StartPointYOffset = 2;

        private readonly Collider[] _hits = new Collider[HitTargetsCount];

        private CharacterAnimator _characterAnimator;
        private CharacterController _characterController;
        private Stats _stats;
        private IInputService _input;
        private int _layerMask;
        private bool _isDisposed;

        public void Initialize(CharacterAnimator characterAnimator, CharacterController characterController)
        {
            _input = AllServices.Container.Single<IInputService>();
            ExceptionValidator.ThrowIfNull(_input, characterAnimator, characterController);
            _characterAnimator = characterAnimator;
            _characterController = characterController;
            
            _layerMask = 1 << LayerMask.NameToLayer(HittableLayerMaskName);
        }

        public void OnUpdate()
        {
            if (_input.AttackButtonUp() && !_characterAnimator.IsAttacking)
                _characterAnimator.PlayAttack();
        }

        public void OnAttackAnimationEventCalled()
        {
            for (int i = 0; i < Hit(); i++)
            {
                Transform hitTarget = _hits[i].transform.parent;
                if (hitTarget.TryGetComponent(out IHealth health)) 
                    health.TakeDamage(_stats.Damage);
            }
        }

        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint() + transform.forward, _stats.DamageRadius, _hits, _layerMask);

        private Vector3 StartPoint() =>
            new Vector3(transform.position.x, _characterController.center.y / StartPointYOffset, transform.position.z);

        public void LoadProgress(PlayerProgress progress)
        {
            _stats = progress.HeroStats;
            ExceptionValidator.ThrowIfNull(_stats);
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
                _characterController = null;
                _stats = null;
                _input = null;
            }

            _isDisposed = true;
        }
        
        ~CharacterAttack()
        {
            Dispose(disposing: false);
        }
    }
}