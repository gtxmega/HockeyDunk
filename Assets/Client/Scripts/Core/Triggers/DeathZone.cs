using UnityEngine;

namespace GameCore.Triggers
{
    public class DeathZone : MonoBehaviour
    {
        private GameMode m_GameMode;

        private void Start() 
        {
            m_GameMode = FindObjectOfType<GameMode>();
        }

        private void OnTriggerEnter(Collider other) 
        {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<Character>().KillCharacter();
                m_GameMode.LevelLose();
            }
        }
    }
}
