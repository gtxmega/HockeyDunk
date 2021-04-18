using UnityEngine;

using GameCore.StateMachines;

namespace GameCore
{

    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CenterOfMassChanger))]
    public class Character : MonoBehaviour, IMovementBehavior, IAnimatorBehavior
    {
        #region Variables

            [SerializeField] private CharacterData m_AttributesData;
            [SerializeField] private Transform m_GroundCheckTransform;
            [SerializeField] private LayerMask m_GroundLayer;


        #endregion

        #region Properties

            public CharacterData m_CharacterData {get {return m_AttributesData;} private set{ m_AttributesData = value;}}

            // State machine and all character state
            public StateMachine m_StateMachine {get; private set;}
            public StandingState m_StandingState {get; private set;}
            public SlipState m_SlipState {get; private set;}
            public JumpState m_JumpState {get; private set;}
            public FlyingState m_FlyingState {get; private set;}

        #endregion

        #region Components

            public Transform m_Transform {get; private set;}

            private Rigidbody m_RigidBody;
            private Animator m_Animator;

        #endregion

        #region  MonoBehavior methods

            private void Start() 
            {
                m_Transform = GetComponent<Transform>();
                m_RigidBody = GetComponent<Rigidbody>();
                m_Animator = GetComponent<Animator>();

                m_StateMachine = new StateMachine();

                m_StandingState = new StandingState(this, this, this, m_StateMachine);
                m_SlipState = new SlipState(this, this, this, m_StateMachine);
                m_JumpState = new JumpState(this, this, this, m_StateMachine);
                m_FlyingState = new FlyingState(this, this, this, m_StateMachine);

                m_StateMachine.Initialize(m_StandingState);
            }

            private void Update()
            {
                m_StateMachine.CurrentState.HandleInput();
                m_StateMachine.CurrentState.LogicUpdate();
            }

            private void FixedUpdate() 
            {
                m_StateMachine.CurrentState.PhysicsUpdate();
            }

        #endregion

        #region  Methods


            #region Interface IMovementBehevior

                public float GetSqrMagnitudeAngularVelocity()
                {
                    return m_RigidBody.angularVelocity.sqrMagnitude;
                }

                public float GetSqrMagnitudeVelocity()
                {
                    return m_RigidBody.velocity.sqrMagnitude;
                }

                public void Movement(Vector3 speedVector)
                {
                    m_RigidBody.AddForce(speedVector, ForceMode.Acceleration);
                }

                public void AddTorque(Vector3 torqueVector)
                {
                    m_RigidBody.AddTorque(torqueVector, ForceMode.Impulse);
                }

                public void ApplyImpulse(Vector3 impulse)
                {      
                    m_RigidBody.velocity = Vector3.zero;            
                    m_RigidBody.AddForce(impulse, ForceMode.Impulse);
                }

                public void ResetTorqueParams()
                {
                    m_RigidBody.angularVelocity = Vector3.zero;
                }

                public void ResetForceParams()
                {
                    m_RigidBody.velocity = Vector3.zero;
                }

                public bool CheckGround()
                {
                    return Physics.OverlapSphere(m_GroundCheckTransform.position, 0.2f, m_GroundLayer).Length > 0;
                }
            #endregion

            #region  Interface AnimatorBehavior
                public void SetAnimationTrigger(int triggerID)
                {
                    m_Animator.SetTrigger(triggerID);
                }

                public void SetAnimatorParamBool(int animID, bool state)
                {
                    m_Animator.SetBool(animID, state);
                }

                public void SetAnimatorParamFloat(int animID, float value)
                {
                    m_Animator.SetFloat(animID, value);
                }

            #endregion
            

        #endregion
    }
}
