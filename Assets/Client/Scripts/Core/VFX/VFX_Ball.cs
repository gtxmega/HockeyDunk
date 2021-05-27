using UnityEngine;

namespace GameCore
{
    public class VFX_Ball : MonoBehaviour
    {
        [SerializeField] private ParticleSystem m_VFXFlame;

        private Character m_Character;
        private GameMode m_GameMode;

        private void Start() 
        {
            m_Character = FindObjectOfType<Character>();
            m_GameMode = FindObjectOfType<GameMode>();

            m_Character.EventDunk.AddListener(EnableVFX);
            m_GameMode.EventRestartLevel.AddListener(DisableVFX);

        }

        public void EnableVFX()
        {
            m_VFXFlame.Play();
        }

        public void DisableVFX()
        {
            m_VFXFlame.Stop();
        }

    }

}
