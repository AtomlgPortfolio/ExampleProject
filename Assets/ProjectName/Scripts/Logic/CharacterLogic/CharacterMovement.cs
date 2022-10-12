using System;
using ProjectName.Scripts.Data;
using ProjectName.Scripts.Data.Extensions;
using ProjectName.Scripts.Infrastructure.Services;
using ProjectName.Scripts.Infrastructure.Services.PersistentProgress;
using ProjectName.Scripts.Services.Inputs;
using ProjectName.Scripts.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProjectName.Scripts.Logic.CharacterLogic
{
    public class CharacterMovement : MonoBehaviour, ISavedProgress, IDisposable
    {
        private CharacterController _characterController;
        private IInputService _inputService;
        private Camera _camera;
        private Stats _stats;
        private bool _isDisposed;
        
        private float MovementSpeed => _stats.MovementSpeed;

        public void Initialize(CharacterController characterController)
        {
            _inputService = AllServices.Container.Single<IInputService>();
            _camera = Camera.main;
            ExceptionValidator.ThrowIfNull(_inputService, characterController, _camera);
            _characterController = characterController;
        }

        public void OnUpdate()
        {
            Vector3 movementVector = Vector3.zero;
            
            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            _characterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVector3Data());

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() != progress.WorldData.PositionOnLevel.Level)
                return;

            Vector3Data savedPosition = progress.WorldData.PositionOnLevel.Position;
            if (savedPosition != null)
                Warp(to: savedPosition);
            
            _stats = progress.HeroStats;
            ExceptionValidator.ThrowIfNull(_stats);
        }

        private string CurrentLevel() =>
            SceneManager.GetActiveScene().name;

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            transform.position = to.AsUnityVector().AddY(_characterController.height);
            _characterController.enabled = true;
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
                _characterController = null;
                _inputService = null;
                _camera = null;
                _stats = null;
            }
            
            _isDisposed = true;
        }

        ~CharacterMovement()
        {
            Dispose(disposing: false);
        }
    }
}