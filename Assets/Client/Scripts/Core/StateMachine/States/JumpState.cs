using UnityEngine;

namespace GameCore.StateMachines
{
    public class JumpState : State
    {
        private int jumpTriggerID = Animator.StringToHash("Jump");

        public JumpState(IMovementBehavior movement,
                                IAnimatorBehavior animator, Character character,
                                        StateMachine stateMachine)
                                            :base(movement, animator, character, stateMachine) {}

        public override void Enter()
        {
            base.Enter();

            Jump();
            m_AnimatorBehevior.ResetAllTriggers();
            m_AnimatorBehevior.SetAnimationTrigger(jumpTriggerID);
            m_Character.m_StateMachine.ChangeState(m_Character.m_FlyingState);
            
        }

        private void Jump()
        {
            var forceVector = m_Character.m_Transform.forward;
            
            forceVector.y += Mathf.Cos(120.0f);
            forceVector = forceVector.normalized;
            forceVector *= m_Character.m_CharacterData.JumpForce;

            m_MovementBehevior.ApplyImpulse(forceVector);
        }

    }

}
