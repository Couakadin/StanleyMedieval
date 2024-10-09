using Data.Runtime;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerAudio : MonoBehaviour
    {
        #region Unity

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_wasPlayed)
            {
                AudioPlay();
            }
        }

        #endregion

    #region Methods

        public void AudioPlay()
        {
            _tmp.GetComponent<TextCleaner>().m_resetTimer = _clip.m_audio.length + 0.5f;
            _audio.clip = _clip.m_audio;
            _audio.Play();
            _tmp.text = _clip.m_text;
            _wasPlayed = true;
        }

    #endregion


        #region Privates

        [Header ("-- Audio --")]
        [SerializeField] private DialogueScriptableObject _clip;
        [SerializeField] private AudioSource _audio;

        [Header("-- Text --")]
        [SerializeField] private TMP_Text _tmp;

        private bool _wasPlayed;

        #endregion
    }
}
