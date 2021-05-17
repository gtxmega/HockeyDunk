using UnityEngine;


namespace GameCore.Triggers
{

    public class RingTrigger : MonoBehaviour
    {
        [SerializeField] private HingeJoint m_HingeJointForRagdoll;
        [SerializeField] private GameObject m_PrefabRagdoll;
        [SerializeField] private GameObject m_PrefabSpawnBall;

        [Header("Ball")]
        [SerializeField] private Transform m_PointSpawnBall;

        private BindingRagdoll m_BindingRagDoll;
        
        private GameObject m_BallObject;

        private GameMode m_GameMode;

        private void Start() 
        {
            m_GameMode = FindObjectOfType<GameMode>();

            m_GameMode.EventRestartLevel.AddListener(ClearBinding);
        }

        private void OnTriggerEnter(Collider other) 
        {
            var character = other.GetComponentInParent<Character>();

            if(character == null)
                return;

            if(other.CompareTag("Ball") && character.m_isGrouping == false)
            {

                m_BallObject = Instantiate(m_PrefabSpawnBall, m_PointSpawnBall.position, Quaternion.identity);

                character.gameObject.SetActive(false);
                
                var ragdoll = Instantiate(m_PrefabRagdoll, character.m_Transform.position, character.m_Transform.rotation);
                
                m_BindingRagDoll = ragdoll.GetComponent<BindingRagdoll>();
                m_BindingRagDoll.ApplyBinding(m_HingeJointForRagdoll);

                character.m_StateMachine.ChangeState(character.m_IdleState);
                m_GameMode.EventLevelWon.Invoke();
                
            }

        }

        public void ClearBinding()
        {
            if(m_BindingRagDoll != null)
            {
                m_HingeJointForRagdoll.connectedBody = null;
                
                Destroy(m_BallObject);
                Destroy(m_BindingRagDoll.gameObject);
            }
        }

        private void OnEnable() 
        {
            if(m_GameMode != null)
            {
                m_GameMode.EventRestartLevel.AddListener(ClearBinding);
            }
        }

        private void OnDisable() 
        {
            if(m_GameMode != null)
            {
                m_GameMode.EventRestartLevel.RemoveListener(ClearBinding);
            }
        }



    }

}