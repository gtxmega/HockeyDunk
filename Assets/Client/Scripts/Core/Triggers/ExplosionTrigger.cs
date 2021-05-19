using UnityEngine;

namespace GameCore.Triggers
{
    public class ExplosionTrigger : MonoBehaviour
    {
        [Header("Parameters")]
        [SerializeField] private float m_ExplosionForce;
        [SerializeField] private float m_ExplosionRadius;

        [Header("Objects")]
        [SerializeField] private Transform m_OriginalObjectTransform;
        [SerializeField] private Rigidbody[] m_Shards;


#if UNITY_EDITOR

        [Header("Only Editor")]
        [SerializeField] private bool m_ShowExplosionRadius;

#endif

        private Vector3[] m_ShardsPositions;
        private Quaternion[] m_ShardsRotations;

#region Components

        private GameMode m_GameMode;

#endregion

#region MonoBehevior Methods

        private void Start() 
        {
            m_ShardsPositions = new Vector3[m_Shards.Length];
            m_ShardsRotations = new Quaternion[m_Shards.Length];

            for(int i = 0; i < m_Shards.Length; ++i)
            {
                m_ShardsPositions[i] = m_Shards[i].position;
                m_ShardsRotations[i] = m_Shards[i].rotation;

                m_Shards[i].isKinematic = true;
                m_Shards[i].gameObject.SetActive(false);
            }

            m_GameMode = FindObjectOfType<GameMode>();

            SubscribeToEvents();
        }

        private void OnEnable() 
        {
            SubscribeToEvents();
        }

        private void OnDisable() 
        {
            UnsubscribeEvents();
        }

#endregion

#region Methods

        public void ApplyExplosion()
        {
            m_OriginalObjectTransform.gameObject.SetActive(false);

            for(int i = 0; i < m_Shards.Length; ++i)
            {
                m_Shards[i].isKinematic = false;
                
                m_Shards[i].gameObject.SetActive(true);

                m_Shards[i].AddExplosionForce(m_ExplosionForce, 
                                                m_OriginalObjectTransform.position,
                                                                        m_ExplosionRadius);
            }
        }

        private void ResetShards()
        {
            for(int i = 0; i < m_Shards.Length; ++i)
            {
                m_Shards[i].isKinematic = true;

                m_Shards[i].position = m_ShardsPositions[i];
                m_Shards[i].rotation = m_ShardsRotations[i];
                
                m_Shards[i].gameObject.SetActive(false);
            }
        }

        private void SubscribeToEvents()
        {
            if(m_GameMode != null)
            {
                m_GameMode.EventLevelWon.AddListener(ApplyExplosion);
            }
        }

        private void UnsubscribeEvents()
        {
            if(m_GameMode != null)
            {
                m_GameMode.EventLevelWon.RemoveListener(ApplyExplosion);
            }
        }

#endregion

#region Visuals for editor
    #if UNITY_EDITOR

        private void OnDrawGizmos() 
        {
            if(m_ShowExplosionRadius && m_OriginalObjectTransform != null)
            {
                Gizmos.color = Color.cyan;
                Gizmos.DrawSphere(m_OriginalObjectTransform.position, m_ExplosionRadius);
            }
        }

    #endif
#endregion

    }

}
