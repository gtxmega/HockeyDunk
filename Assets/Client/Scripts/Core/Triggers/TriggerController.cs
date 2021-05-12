using UnityEngine;

namespace GameCore.Triggers
{
    public class TriggerController : MonoBehaviour
    {
        private ForceTrigger[] m_AllTriggers;

        private GameMode m_GameMode;

        private void Start() 
        {
            var triggers = FindObjectsOfType<ForceTrigger>();
            m_AllTriggers = triggers;

            m_GameMode = FindObjectOfType<GameMode>();

            AddListenerEvents();
        }

        public void ResetAllTriggers()
        {
            for(int i = 0; i < m_AllTriggers.Length; ++i)
            {
                m_AllTriggers[i].gameObject.SetActive(true);
            }
        }

        private void AddListenerEvents()
        {
            if(m_GameMode != null)
            {
                m_GameMode.EventRestartLevel.AddListener(ResetAllTriggers);
            }
        }

        private void RemoveListenerEvents()
        {
            m_GameMode.EventRestartLevel.RemoveListener(ResetAllTriggers);
        }


        private void OnEnable() 
        {
            AddListenerEvents();
        }

        private void OnDisable() 
        {
            RemoveListenerEvents();
        }


    }
}
