using UnityEngine;

namespace ProjectName.Scripts.Logic.CharacterLogic
{
    public class CharacterAnimatorHashes
    {
        public readonly int MoveHash = Animator.StringToHash("Walking");
        public readonly int AttackHash = Animator.StringToHash("AttackNormal");
        public readonly int HitHash = Animator.StringToHash("Hit");
        public readonly int DieHash = Animator.StringToHash("Die");

        public readonly int IdleStateHash = Animator.StringToHash("Idle");
        public readonly int AttackStateHash = Animator.StringToHash("Attack");
        public readonly int WalkingStateHash = Animator.StringToHash("Walking");
        public readonly int DeathStateHash = Animator.StringToHash("Die");
    }
}