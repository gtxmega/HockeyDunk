using UnityEngine;


namespace GameCore.Triggers
{

    public class ForceTrigger : MonoBehaviour
    {
        private enum TRIGGER_TYPE
        {
            FORCE,
            TRAMPOLINE
        }

        [SerializeField] private float m_Force;
        [SerializeField] private TRIGGER_TYPE m_TriggerType;

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
            var character = otherCollider.GetComponentInParent<Character>();

            if(character == null)
                return;

            switch(m_TriggerType)
            {
                case TRIGGER_TYPE.FORCE:

                    character.ApplyImpulse(forceVector);

                break;
                case TRIGGER_TYPE.TRAMPOLINE:

                    if(character.m_isGrouping == false)
                    {
                        character.ApplyImpulse(forceVector);
                        gameObject.SetActive(false);
                    }else
                    {
                        character.KillCharacter();
                    }
                    
                break;
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
