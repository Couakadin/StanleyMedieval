using Data.Runtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class FXReader : MonoBehaviour
    {
        #region PUBLICS

        public AudioSource m_audioSource;

        #endregion


        #region MAIN METHODS

        public void AudioPlay(AudioClip clip)
        {
            if (clip != null)
            {
                m_audioSource.clip = clip;
                m_audioSource.Play(); 
            }
        }

        #endregion
    }
}
