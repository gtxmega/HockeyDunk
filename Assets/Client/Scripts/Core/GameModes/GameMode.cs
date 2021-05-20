using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameCore
{
    public class GameMode : MonoBehaviour
    {
        #region Events

            [HideInInspector] public UnityEvent EventStartLevel = new UnityEvent();
            public UnityEvent EventLevelWon = new UnityEvent();
            [HideInInspector] public UnityEvent EventLevelLose = new UnityEvent();
            [HideInInspector] public UnityEvent EventRestartLevel = new UnityEvent();

        #endregion

        #region Variables

            [SerializeField] private float m_TimeToStartLevel;
            private float m_CurrentTimerStartLevel;

            [SerializeField] private Transform m_TransformStartPoint;

        #endregion

        #region Components

            private Character m_Character;
            private CameraFollow m_CameraFollow;

        #endregion

        #region MenoBehevior Methods

            private void Start() 
            {
                m_Character = FindObjectOfType<Character>();
                m_CameraFollow = FindObjectOfType<CameraFollow>();

                AddListenerEvents();

                StartLevel();
            }

        #endregion

        #region Methods

            public void StartLevel()
            {
                EventStartLevel.Invoke();
                StartCoroutine(TimerStartLevel());
            }

            public void RestartLevel()
            {
                m_Character.ReinitializeCharacter();

                m_CameraFollow.UpdateFollowPosition();

                EventRestartLevel.Invoke();
                StartCoroutine(TimerStartLevel());
            }

            public void LevelLose()
            {
                EventLevelLose.Invoke();
            }

            private IEnumerator TimerStartLevel()
            {
                m_CurrentTimerStartLevel = m_TimeToStartLevel;

                while(m_CurrentTimerStartLevel >= 0.0f)
                {
                    m_CurrentTimerStartLevel -= Time.deltaTime;
                    yield return null;
                }

                m_Character.m_StateMachine.ChangeState(m_Character.m_StandingState);

            }

            public float GetTimerStartLevel()
            {
                return m_CurrentTimerStartLevel;
            }


            private void AddListenerEvents()
            {
                if(m_Character != null)
                {
                    m_Character.EventDeath.AddListener(LevelLose);
                }
            }

            private void RemoveListenerEvents()
            {
                if(m_Character != null)
                {
                    m_Character.EventDeath.RemoveListener(LevelLose);
                }
            }

            private void OnEnable() 
            {
                AddListenerEvents();
            }

            private void OnDisable() 
            {
                RemoveListenerEvents();
            }

        #endregion
    }
}
