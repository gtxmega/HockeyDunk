using System.Collections;
using UnityEngine;

namespace GameCore
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float m_Duration;
        [SerializeField] private float m_Magnitude;

        private Transform m_Transform;
        private GameMode m_GameMode;

        private void Start() 
        {
            m_Transform = GetComponent<Transform>();

            m_GameMode = FindObjectOfType<GameMode>();

            SubscribeEvents();
        }
        
        public void StartShaker()
        {
            StartCoroutine(Shake());
        }

        private IEnumerator Shake()
        {
            float timer = 0.0f;

            while(timer < m_Duration)
            {
                float x = Random.Range(-1.0f, 1.0f) * m_Magnitude;
                float y = Random.Range(-1.0f, 1.0f) * m_Magnitude;

                m_Transform.position += new Vector3(x, y, 0.0f);

                timer += Time.deltaTime;

                yield return null;
            }
        }

        private void SubscribeEvents()
        {
            if(m_GameMode != null)
            {
                m_GameMode.EventLevelWon.AddListener(StartShaker);
            }
        }

        private void UnsubscribeEvents()
        {
            if(m_GameMode != null)
            {
                m_GameMode.EventLevelWon.RemoveListener(StartShaker);
            }
        }

        private void OnEnable() 
        {
            SubscribeEvents();
        }

        private void OnDisable() 
        {
            UnsubscribeEvents();
        }

    }

}
