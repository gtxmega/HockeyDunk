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
        
        [SerializeField] private ForceType m_ForceType;
        [SerializeField] private float m_Force;

        private Vector3 forceVector;

        private void Start() 
        {
            forceVector = transform.forward;
            forceVector.y += Mathf.Cos(120.0f);

            forceVector = forceVector.normalized;
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

            gameObject.SetActive(false);
        }

#region  Visuals for editor

    #if UNITY_EDITOR

        [Header("Only Editor")]
        [SerializeField] private Color m_ColorRay;

        private void OnDrawGizmos() 
        {
            Gizmos.color = m_ColorRay;

            var direction = transform.forward;
            direction.y += Mathf.Cos(120.0f);
            direction = direction.normalized;
            direction *= 10.0f;

            Gizmos.DrawRay(transform.position, direction);
        }

    #endif

#endregion

    }

}
