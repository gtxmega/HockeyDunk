using System.Collections;
using UnityEngine;


namespace GameCore.VFX
{
    public class VFX_JumpTrail : MonoBehaviour
    {
        [SerializeField] private float m_TimerShow;

        private TrailRenderer[] m_TrailObjects;

        private Coroutine m_TimerShowUpdateCoroutine;

        private void Start() 
        {
            m_TrailObjects = GetComponentsInChildren<TrailRenderer>();
            TrailsEnableOrDisable(false);
        }

        public void PlayVFXJump()
        {
            if(m_TimerShowUpdateCoroutine == null)
                m_TimerShowUpdateCoroutine = StartCoroutine(TimerShowUpdate());
        }

        public void TrailsEnableOrDisable(bool status)
        {
            for(int i = 0; i < m_TrailObjects.Length; ++i)
            {
                m_TrailObjects[i].gameObject.SetActive(status);
            }
        }

        private IEnumerator TimerShowUpdate()
        {
            TrailsEnableOrDisable(true);
            var timer = 0.0f;

            while(timer <= m_TimerShow)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            TrailsEnableOrDisable(false);
            m_TimerShowUpdateCoroutine = null;
        }
    }
}
