using UnityEngine;

namespace GameCore.StateMachines
{
    public class FlyingState : DeathState
    {
        private int flyingTriggerID = Animator.StringToHash("Flying");
        private int rotationSpeedID = Animator.StringToHash("RotationSpeed");

        private CameraFollow m_CameraFollow;

        public FlyingState(IMovementBehavior movement,
                                IAnimatorBehavior animator, Character character,
                                        StateMachine stateMachine)
                                            :base(movement, animator, character, stateMachine) 
                                            {
                                                m_CameraFollow = GameObject.FindObjectOfType<CameraFollow>();
                                            }

        public override void Enter()
        {
            base.Enter();

            m_CameraFollow.EnableCameraSlow();

            m_Character.m_CenterOfMassChanger.ChangeCenterOfMass(E_COM_TYPE.FLYING);
            
            m_AnimatorBehevior.ResetAllTriggers();
            m_AnimatorBehevior.SetAnimationTrigger(flyingTriggerID);
        }

        public override void HandleInput()
        {
            base.HandleInput();

            if(Input.GetMouseButton(0))
            {
                m_Character.m_CenterOfMassChanger.ChangeCenterOfMass(E_COM_TYPE.GROUPING);
                m_MovementBehevior.AddTorque(Vector3.right * m_Character.m_CharacterData.TrunSpeed);
                
                m_MovementBehevior.SetGrouping(true);
            }

            if(Input.GetMouseButtonUp(0))
            {
                m_Character.m_CenterOfMassChanger.ChangeCenterOfMass(E_COM_TYPE.FLYING);
                m_MovementBehevior.SetGrouping(false);
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

            m_CameraFollow.DisableCameraSlow();
            m_AnimatorBehevior.SetAnimatorParamFloat(rotationSpeedID, 0.0f);
        }


    }

}
