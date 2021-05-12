using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GameCore
{
    public class mainHUD : MonoBehaviour
    {

        #region Variables

            [Header("UI Links")]
            [SerializeField] private GameObject m_GameLosePanel;
            [SerializeField] private GameObject m_StartLevelPanel;
            [SerializeField] private GameObject m_LevelWonPanel;
            [SerializeField] private Text m_TextTimerStart;

        #endregion

        #region Components

            private GameMode m_GameMode;

        #endregion


        #region MonoBehavior Methods

            private void Start() 
            {
                m_GameMode = FindObjectOfType<GameMode>();

                AddListenerEvents();
            }

        #endregion

        #region Methods

            public void ShowLevelStartPanel()
            {
                HideLevelWonPanel();
                HideGameLoseScreen();

                m_StartLevelPanel.SetActive(true);
                StartCoroutine(UpdateTimerLevelStart());
            }

            private IEnumerator UpdateTimerLevelStart()
            {
                yield return new WaitForSeconds(0.5f);

                while(m_GameMode.GetTimerStartLevel() >= 0.0f)
                {
                    m_TextTimerStart.text = Mathf.RoundToInt(m_GameMode.GetTimerStartLevel()).ToString();
                    yield return null;
                }

                m_StartLevelPanel.SetActive(false);
            }

            public void ShowLevelWonPanel()
            {
                m_LevelWonPanel.SetActive(true);
            }
            public void HideLevelWonPanel()
            {
                m_LevelWonPanel.SetActive(false);
            }

            public void ShowGameLoseScreen()
            {
                m_GameLosePanel.SetActive(true);
            }

            public void HideGameLoseScreen()
            {
                m_GameLosePanel.SetActive(false);
            }

            private void AddListenerEvents()
            {
                if(m_GameMode != null)
                {
                    m_GameMode.EventLevelLose.AddListener(ShowGameLoseScreen);
                    
                    m_GameMode.EventRestartLevel.AddListener(HideGameLoseScreen);
                    m_GameMode.EventRestartLevel.AddListener(ShowLevelStartPanel);

                    m_GameMode.EventStartLevel.AddListener(ShowLevelStartPanel);

                    m_GameMode.EventLevelWon.AddListener(ShowLevelWonPanel);
                }
            }
            private void RemoveListenerEvents()
            {
                if(m_GameMode != null)
                {
                    m_GameMode.EventLevelLose.RemoveListener(ShowGameLoseScreen);

                    m_GameMode.EventRestartLevel.RemoveListener(HideGameLoseScreen);
                    m_GameMode.EventRestartLevel.RemoveListener(ShowLevelStartPanel);

                    m_GameMode.EventStartLevel.RemoveListener(ShowLevelStartPanel);

                    m_GameMode.EventLevelWon.RemoveListener(ShowLevelWonPanel);
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
