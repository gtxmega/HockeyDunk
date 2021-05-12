using UnityEngine;

namespace GameCore.StateMachines
{
    public class FlyingState : DeathState
    {
        private int flyingTriggerID = Animator.StringToHash("Flying");
        private int rotationSpeedID = Animator.StringToHash("RotationSpeed");

        public FlyingState(IMovementBehavior movement,
                                IAnimatorBehavior animator, Character character,
                                        StateMachine stateMachine)
                                            :base(movement, animator, character, stateMachine) {}

        public override void Enter()
        {
            base.Enter();
            
            m_AnimatorBehevior.ResetAllTriggers();
            m_AnimatorBehevior.SetAnimationTrigger(flyingTriggerID);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(Input.touchCount > 0)
            {
                var currentTouch = Input.GetTouch(0);

                switch(currentTouch.phase)
                {
                    case TouchPhase.Stationary:
                        m_MovementBehevior.AddTorque(Vector3.right * 3.5f);
                    break;
                }
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

            m_AnimatorBehevior.SetAnimatorParamFloat(rotationSpeedID, m_MovementBehevior.GetSqrMagnitudeAngularVelocity());

            if(m_MovementBehevior.CheckGround())
            {
                m_StateMachine.ChangeState(m_Character.m_SlipState);
            }
        }

        public override void Exit()
        {
            base.Exit();

            m_AnimatorBehevior.SetAnimatorParamFloat(rotationSpeedID, 0.0f);
        }


    }

}
