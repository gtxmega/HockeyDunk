using UnityEngine;

namespace GameCore
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 m_CameraOffset;
        [SerializeField] private float m_CameraSmooth;

        [Header("Target follow")]
        [SerializeField] private Transform m_TargetFollow;

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
        }

        private void LateUpdate()
        {
            var _deltaPosition = m_TargetFollow.position + m_CameraOffset;
            m_Transform.position = Vector3.Lerp(m_Transform.position, _deltaPosition, m_CameraSmooth);

        }

#endregion

#region Methods

    public void SetTargetFollow(Transform targetFollow)
    {
        m_TargetFollow = targetFollow;
    }

#endregion

    }
}
