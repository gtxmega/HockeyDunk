using UnityEngine;


namespace GameCore.Triggers
{

    public class ForceTrigger : MonoBehaviour
    {
        private enum ForceType
        {
            FORCE,
            IMPULSE
        }

        [SerializeField] private Transform m_PintDirectionTransform;
        [SerializeField] private ForceType m_ForceType;
        [SerializeField] private float m_Force;

        private Vector3 forceVector;

        private void Start() 
        {
            forceVector = (m_PintDirectionTransform.position - transform.position).normalized;
            forceVector *= m_Force;
        }

        private void OnTriggerEnter(Collider otherCollider) 
        {
            var _movementBehavior = otherCollider.GetComponentInParent<IMovementBehavior>();

            if(_movementBehavior != null)
            {
                switch(m_ForceType)
                {
                    case ForceType.FORCE:
                        _movementBehavior.Movement(forceVector);
                    break;
                    case ForceType.IMPULSE:
                        _movementBehavior.ApplyImpulse(forceVector);
                    break;
                }
            }
        }

#region  Visuals for editor

    #if UNITY_EDITOR

        [Header("Only Editor")]
        [SerializeField] private Color m_ColorRay;

        private void OnDrawGizmos() 
        {
            Gizmos.color = m_ColorRay;

            if(m_PintDirectionTransform != null)
                Gizmos.DrawLine(transform.position, m_PintDirectionTransform.position);
        }

    #endif

#endregion

    }

}
