using UnityEngine;

using Random = System.Random;
using GameCore.Triggers;

namespace GameCore.StateMachines
{
    public class FlyingState : DeathState
    {
        private int flyingTriggerID = Animator.StringToHash("Flying");
        private int rotationSpeedID = Animator.StringToHash("RotationSpeed");

        private bool m_IsDunkGrouping;

        private int m_RandomIndexPos;
        private Random m_Randomizer = new Random();

        private CameraFollow m_CameraFollow;

        private RingTrigger m_RingTrigger;

        public FlyingState(IMovementBehavior movement,
                                IAnimatorBehavior animator, Character character,
                                        StateMachine stateMachine)
                                            :base(movement, animator, character, stateMachine) 
                                            {
                                                m_CameraFollow = GameObject.FindObjectOfType<CameraFollow>();
                                                m_RingTrigger = GameObject.FindObjectOfType<RingTrigger>();
                                            }

        public override void Enter()
        {
            base.Enter();

            m_CameraFollow.EnableCameraSlow();

            m_Character.m_CenterOfMassChanger.ChangeCenterOfMass(E_COM_TYPE.FLYING);
            
            m_AnimatorBehevior.ResetAllTriggers();
            
            m_AnimatorBehevior.SetAnimationTrigger(flyingTriggerID);

            m_RandomIndexPos = m_Randomizer.Next(0, 4);
        }

        float timer;

        public override void HandleInput()
        {
            base.HandleInput();

            if(Input.GetMouseButton(0))
            {
                m_Character.m_CenterOfMassChanger.ChangeCenterOfMass(E_COM_TYPE.GROUPING);
                m_MovementBehevior.AddTorque(Vector3.right * m_Character.m_CharacterData.TrunSpeed);

                timer += Time.deltaTime;
                var period = 2 * Mathf.PI / m_Character.GetAngularVelocity();

                m_AnimatorBehevior.SetAnimationTrigger(Animator.StringToHash("Grouping"));


                if(m_IsDunkGrouping)
                {                    
                    m_AnimatorBehevior.SetAnimatorParamFloat(rotationSpeedID, 0);
                    m_MovementBehevior.SetGrouping(false);

                }else
                {
                    m_AnimatorBehevior.SetAnimatorParamFloat(rotationSpeedID, m_RandomIndexPos);

                    m_MovementBehevior.SetGrouping(true);
                }
                
            }

            if(Vector3.Distance(m_Character.m_Transform.position, m_RingTrigger.transform.position) < 7.0f)
            {
                m_AnimatorBehevior.SetAnimationTrigger(Animator.StringToHash("Ungrouping"));
            }



            if(Input.GetMouseButtonUp(0))
            {
                timer = 0.0f;

                var randomNumber = m_Randomizer.Next(0, 4);
                while(m_RandomIndexPos == randomNumber)
                    randomNumber = m_Randomizer.Next(0, 4);

                m_RandomIndexPos = randomNumber;

                m_Character.m_CenterOfMassChanger.ChangeCenterOfMass(E_COM_TYPE.FLYING);
                m_MovementBehevior.SetGrouping(false);

                m_AnimatorBehevior.ResetAllTriggers();
                m_AnimatorBehevior.SetAnimationTrigger(Animator.StringToHash("Ungrouping"));
            }
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();

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
            m_MovementBehevior.SetGrouping(false);

            m_IsDunkGrouping = false;
        }


        public void SetDunkGrouping(bool state)
        {
            m_AnimatorBehevior.SetAnimationTrigger(Animator.StringToHash("Dunk"));
            m_IsDunkGrouping = state;
        }


    }

}
