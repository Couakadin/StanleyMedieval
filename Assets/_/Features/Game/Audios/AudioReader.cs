using Data.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class AudioReader : MonoBehaviour
    {
        #region Publics

        public float m_audioLength;
        public TMP_Text m_tmp;
        public AudioSource m_audioSource;
        public bool m_isPlayingAudio;
        public List<DialogueScriptableObject> m_clipsToRead;

        #endregion

        #region

        private void Update()
        {
            if (m_audioLength <= 0)
            {

                if (_clipIndex < m_clipsToRead.Count && !m_isPlayingAudio)
                {
                    AudioPlay(m_clipsToRead[_clipIndex]);
                    m_isPlayingAudio = true;
                }
                else
                    m_isPlayingAudio = false;
            }
            else
            {
                    m_audioLength -= Time.deltaTime;
                    m_isPlayingAudio = true;
            }
        }

        #endregion

        #region MAIN METHODS

        public void AudioPlay(DialogueScriptableObject clip)
        {
            if (clip.m_audio != null)
                m_audioLength = clip.m_audio.length + 0.5f;
            else
                m_audioLength = 7.5f;

            m_tmp.GetComponent<TextCleaner>().m_resetTimer = m_audioLength;
            m_audioSource.clip = clip.m_audio;
            m_audioSource.Play();
            m_tmp.text = clip.m_text;
            m_isPlayingAudio = true;
            _clipIndex += 1;
        }

        public void AudioSet(List<DialogueScriptableObject> clips)
        {
            m_clipsToRead = clips;
            if (clips.Count > 0)
                AudioPlay(m_clipsToRead[0]);
        }
        #endregion

        #region PIVATES

        private int _clipIndex;

        #endregion
    }
}
