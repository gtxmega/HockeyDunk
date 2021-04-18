using UnityEngine;

namespace GameCore
{
    public class BindingRagdoll : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_RigidBodyBinding;

        public void ApplyBinding(HingeJoint hingeJoint)
        {
            hingeJoint.connectedBody = m_RigidBodyBinding;
        }
    }
}
