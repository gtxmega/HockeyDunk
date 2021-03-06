using UnityEngine;
using UnityEngine.Events;

using GameCore.StateMachines;

namespace GameCore
{

    [RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CenterOfMassChanger))]
    public class Character : MonoBehaviour, IMovementBehavior, IAnimatorBehavior
    {
        #region Events

        [HideInInspector] public UnityEvent EventDeath = new UnityEvent();
        [HideInInspector] public UnityEvent EventDunk = new UnityEvent();

        #endregion

        #region Variables

            [SerializeField] private CharacterData m_AttributesData;
            [SerializeField] private Transform m_GroundCheckTransform;
            [SerializeField] private LayerMask m_GroundLayer;

            [Header("Ball")]
            [SerializeField] private Transform m_SocketBall;
            [SerializeField] private Transform m_BallInCharacter;

            private Vector3 m_StartPosition;
            private Vector3 m_BallStartPosition;

        #endregion

        #region Properties

            public bool m_isDeath {get; private set;}
            public bool m_isGrouping {get; private set;}

            public CharacterData m_CharacterData {get {return m_AttributesData;} private set{ m_AttributesData = value;}}

            public CenterOfMassChanger m_CenterOfMassChanger {get; private set;}

            // State machine and all character state
            public StateMachine m_StateMachine {get; private set;}
            public StandingState m_StandingState {get; private set;}
            public SlipState m_SlipState {get; private set;}
            public JumpState m_JumpState {get; private set;}
            public FlyingState m_FlyingState {get; private set;}
            public IdleState m_IdleState {get; private set;}
            public DunkState m_DunkState {get; private set;}

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

                m_CenterOfMassChanger = GetComponent<CenterOfMassChanger>();

                m_StateMachine = new StateMachine();

                m_StandingState = new StandingState(this, this, this, m_StateMachine);
                m_SlipState = new SlipState(this, this, this, m_StateMachine);
                m_JumpState = new JumpState(this, this, this, m_StateMachine);
                m_FlyingState = new FlyingState(this, this, this, m_StateMachine);
                m_IdleState = new IdleState(this, this, this, m_StateMachine);
                m_DunkState = new DunkState(this, this, this, m_StateMachine);

                m_StateMachine.Initialize(m_IdleState);

                m_StartPosition = m_Transform.position;
                m_BallStartPosition = m_BallInCharacter.localPosition;
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

                public float GetAngularVelocity()
                {
                    return m_RigidBody.angularVelocity.magnitude;
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
                    m_RigidBody.AddTorque(torqueVector, ForceMode.VelocityChange);
                }

                public void ApplyImpulse(Vector3 impulse)
                {
                    ResetForceParams();
                    m_RigidBody.AddForce(impulse, ForceMode.VelocityChange);
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
                    return Physics.OverlapSphere(m_GroundCheckTransform.position, 0.5f, m_GroundLayer).Length > 0;
                }

                public void SetGrouping(bool status)
                {
                    m_isGrouping = status;
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

                public void ResetAllTriggers()
                {
                    m_Animator.ResetTrigger("Run");
                    m_Animator.ResetTrigger("Idle");
                    m_Animator.ResetTrigger("Jump");
                    m_Animator.ResetTrigger("Flying");
                    m_Animator.ResetTrigger("Grouping");
                    m_Animator.ResetTrigger("Ungrouping");
                }

            #endregion
            
            public void KillCharacter()
            {
                ResetForceParams();
                ResetTorqueParams();

                m_isDeath = true;
                m_StateMachine.ChangeState(m_IdleState);

                EventDeath.Invoke();


                m_SocketBall.DetachChildren();
                m_BallInCharacter.GetComponent<Rigidbody>().isKinematic = false;

                m_Animator.enabled = false;

            }

            public void ReinitializeCharacter()
            {
                ResetAllTriggers();
                ResetForceParams();
                ResetTorqueParams();

                m_Transform.position = m_StartPosition;
                m_Transform.rotation = Quaternion.identity;

                m_Animator.enabled = true;
                SetAnimationTrigger(Animator.StringToHash("Idle"));

                m_BallInCharacter.SetParent(m_SocketBall.transform);
                m_BallInCharacter.localPosition = m_BallStartPosition;
                m_BallInCharacter.GetComponent<Rigidbody>().isKinematic = true;

                gameObject.SetActive(true);
            }

        #endregion
    }
}
