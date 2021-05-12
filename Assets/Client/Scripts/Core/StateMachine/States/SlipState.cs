using UnityEngine;

namespace GameCore.StateMachines
{

    public class SlipState : DeathState
    {
        private int slipTriggerID = Animator.StringToHash("Run");
        private int flyingTriggerID = Animator.StringToHash("Jump");

        public SlipState(IMovementBehavior movement,
                                IAnimatorBehavior animator, Character character,
                                        StateMachine stateMachine)
                                            :base(movement, animator, character, stateMachine) {}

        public override void Enter()
        {
            base.Enter();
            
            m_AnimatorBehevior.ResetAllTriggers();
            m_AnimatorBehevior.SetAnimationTrigger(slipTriggerID);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(Input.touchCount > 0)
            {
                var currentTouch = Input.GetTouch(0);

                if(currentTouch.phase == TouchPhase.Ended)
                {
                    m_StateMachine.ChangeState(m_Character.m_JumpState);

                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            if(m_MovementBehevior.CheckGround() == false)
            {
                m_Character.m_StateMachine.ChangeState(m_Character.m_FlyingState);
            }

            var forceVector = m_Character.m_Transform.forward;
            forceVector *= m_Character.m_CharacterData.SlipForce;
            m_MovementBehevior.Movement(forceVector);
        }

    }

}
