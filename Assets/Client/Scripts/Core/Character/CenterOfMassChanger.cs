using UnityEngine;

namespace GameCore
{

    [RequireComponent(typeof(Rigidbody))]
    public class CenterOfMassChanger : MonoBehaviour
    {
        [SerializeField] private Transform m_CenterOfMassDefaultTransform;
        [SerializeField] private Transform m_CenterOfMassGroupingTransform;
        [SerializeField] private Transform m_CenterOfMassFlyingTransform;

        private Rigidbody m_RigidBody;

        private void Awake() 
        {
            m_RigidBody = GetComponent<Rigidbody>();

            ChangeCenterOfMass(E_COM_TYPE.DEFAULT);
        }

        public void ChangeCenterOfMass(E_COM_TYPE state)
        {
            switch(state)
            {
                case E_COM_TYPE.DEFAULT:
                    m_RigidBody.centerOfMass = m_CenterOfMassDefaultTransform.localPosition;
                break;
                case E_COM_TYPE.GROUPING:
                    m_RigidBody.centerOfMass = m_CenterOfMassGroupingTransform.localPosition;
                break;
                case E_COM_TYPE.FLYING:
                    m_RigidBody.centerOfMass = m_CenterOfMassFlyingTransform.localPosition;
                break;
            }
        }

        public Transform GetCenterOfMass(E_COM_TYPE state)
        {
            switch(state)
            {
                case E_COM_TYPE.DEFAULT: return m_CenterOfMassDefaultTransform;
                case E_COM_TYPE.FLYING: return m_CenterOfMassFlyingTransform;
                case E_COM_TYPE.GROUPING: return m_CenterOfMassGroupingTransform;
            }

            throw new System.Exception(name + ": GetCenterOfMass return null, param: " + state);
        }


    }

}
