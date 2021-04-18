using UnityEngine;


namespace GameCore.Triggers
{

    public class RingTrigger : MonoBehaviour
    {
        [SerializeField] private HingeJoint m_HingeJointForRagdoll;
        [SerializeField] private GameObject m_PrefabRagdoll;


        private void OnTriggerEnter(Collider other) 
        {
            var character = other.GetComponentInParent<Character>();

            if(other.CompareTag("Ball") && character != null)
            {
                other.transform.SetParent(null);
                other.gameObject.AddComponent<Rigidbody>();

                character.gameObject.SetActive(false);
                
                var ragdoll = Instantiate(m_PrefabRagdoll, character.m_Transform.position, character.m_Transform.rotation);
                ragdoll.GetComponent<BindingRagdoll>().ApplyBinding(m_HingeJointForRagdoll);
                
            }

        }



    }

}