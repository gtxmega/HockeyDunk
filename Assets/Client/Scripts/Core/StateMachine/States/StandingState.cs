using UnityEngine;

namespace GameCore.StateMachines
{
    public class StandingState : State
    {
        private int idleTriggerID = Animator.StringToHash("Idle");


        public StandingState(IMovementBehavior movement,
                                IAnimatorBehavior animator, Character character,
                                        StateMachine stateMachine):base(movement, animator, character, stateMachine) {}

        public override void Enter()
        {
            base.Enter();
            
            m_AnimatorBehevior.ResetAllTriggers();
            m_AnimatorBehevior.SetAnimationTrigger(idleTriggerID);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(Input.GetMouseButtonDown(0))
            {
                m_StateMachine.ChangeState(m_Character.m_SlipState);
            }
        }

    }
}
