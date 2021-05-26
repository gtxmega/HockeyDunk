using UnityEngine;

namespace GameCore.Triggers
{
    public class TriggerDunk : MonoBehaviour
    {


        private void OnTriggerEnter(Collider otherCollider) 
        {

            var character = otherCollider.GetComponentInParent<Character>();

            if(character != null)
            {
                character.m_StateMachine.ChangeState(character.m_DunkState);
            }
        }
    }

}
