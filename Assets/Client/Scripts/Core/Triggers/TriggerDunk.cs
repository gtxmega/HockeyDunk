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
                character.m_FlyingState.SetDunkGrouping(true);
                character.EventDunk.Invoke();
            }
        }
    }

}
