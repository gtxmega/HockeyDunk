using UnityEngine;

namespace GameCore
{

    [ExecuteInEditMode]
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 m_CameraOffset;
        [SerializeField] private float m_CameraSmoothSpeed;
        [SerializeField] private float m_CameraSlowSmoothSpeed;

        [Header("Target follow")]
        [SerializeField] private Transform m_TargetFollow;



        private float m_CurrentSmoothSpeed;
        private Transform m_Transform;

#region MonoBehavior methods

        private void Start()
        {
            m_Transform = GetComponent<Transform>();

            if (m_TargetFollow == null)
            {
                Debug.Log(this.name + ": Target follow not set");
                
                gameObject.SetActive(false);
            }

            m_CurrentSmoothSpeed = m_CameraSmoothSpeed;

#if UNITY_EDITOR
            updateInEditor = false;
#endif

        }

#if UNITY_EDITOR

        [Tooltip("Update camera position in editor mode")]
        [Header("Update camera position")]
        [SerializeField] private bool updateInEditor;

        private void Update() 
        {
            if(updateInEditor)
                UpdateFollowPosition();
        }
#endif

        private void FixedUpdate()
        {
            UpdateFollowPosition();
        }

#endregion

#region Methods

    public void SetTargetFollow(Transform targetFollow)
    {
        m_TargetFollow = targetFollow;
    }

    public void UpdateFollowPosition()
    {
        var _deltaPosition = m_TargetFollow.position + m_CameraOffset;
        m_Transform.position = Vector3.Lerp(m_Transform.position, _deltaPosition, m_CurrentSmoothSpeed * Time.fixedDeltaTime);
    }

    public void EnableCameraSlow()
    {
        m_CurrentSmoothSpeed = m_CameraSlowSmoothSpeed;
    }

    public void DisableCameraSlow()
    {
        m_CurrentSmoothSpeed = m_CameraSmoothSpeed;
    }

#endregion

    }
}
