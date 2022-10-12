using UnityEngine;

namespace ProjectName.Scripts.Logic.CharacterLogic
{
    [RequireComponent(typeof(CharacterAnimator))]
    [RequireComponent(typeof(CharacterAttack))]
    [RequireComponent(typeof(CharacterHealth))]
    [RequireComponent(typeof(CharacterMovement))]
    [RequireComponent(typeof(CharacterDeath))]
    [RequireComponent(typeof(CharacterController))]
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        private CharacterAnimator _characterAnimator;
        private CharacterAttack _characterAttack;
        private CharacterHealth _characterHealth;
        private CharacterMovement _characterMovement;
        private CharacterDeath _characterDeath;
        private CharacterController _characterController;
        private Animator _animator;

        public void Initialize()
        {
            GetComponents();
            InitializeComponents();
        }

        public void Activate() => gameObject.SetActive(true);

        private void GetComponents()
        {
            _characterAnimator = GetComponent<CharacterAnimator>();
            _characterAttack = GetComponent<CharacterAttack>();
            _characterHealth = GetComponent<CharacterHealth>();
            _characterMovement = GetComponent<CharacterMovement>();
            _characterDeath = GetComponent<CharacterDeath>();
            _characterController = GetComponent<CharacterController>();
            _animator = GetComponent<Animator>();
        }

        private void InitializeComponents()
        {
            _characterAnimator.Initialize(_animator);
            _characterAttack.Initialize(_characterAnimator, _characterController);
            _characterHealth.Initialize(_characterAnimator);
            _characterMovement.Initialize(_characterController);
            _characterDeath.Initialize(_characterHealth, _characterMovement, _characterAnimator, _characterAttack);
        }

        private void Update()
        {
            _characterAttack.OnUpdate();
            _characterMovement.OnUpdate();
        }

        private void OnDestroy()
        {
            _characterDeath?.Dispose();
            _characterAnimator?.Dispose();
            _characterAttack?.Dispose();
            _characterHealth?.Dispose();
            _characterMovement?.Dispose();
        }
    }
}
