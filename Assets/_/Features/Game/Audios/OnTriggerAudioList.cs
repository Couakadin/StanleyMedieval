using Data.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerAudioList : MonoBehaviour
    {
        #region Unity

        private void Update()
        {
            if (!_wasPlayed)
            {
                if (_startedPlaying && !_isPlaying && m_clipIndex < _clips.Count && _audioLength <= 0)
                {
                    print(m_clipIndex);
                    AudioPlay(m_clipIndex);
                    m_clipIndex += 1;
                }

                if (m_clipIndex == _clips.Count)
                    _wasPlayed = true;
            }

            if (_audioLength < 0)
                _isPlaying = false;

            _audioLength -= Time.deltaTime;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_wasPlayed && _sharedIndex == null)
            {
                _startedPlaying = true;
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_wasPlayed && _sharedIndex != null)
            {
                print(m_clipIndex);
                AudioPlay(m_clipIndex);
                m_clipIndex += 1;

                foreach (OnTriggerAudioList otherAudio in _sharedIndex)
                    otherAudio.m_clipIndex = m_clipIndex;

                _wasPlayed = true;
            }
        }

        #endregion

        #region Methods

        public void AudioPlay(int clip)
        {
            if (_clips[clip].m_audio != null)
                _audioLength = _clips[clip].m_audio.length + 0.5f;
            else
                _audioLength = 7.5f;

            _tmp.GetComponent<TextCleaner>().m_resetTimer = _audioLength;
            _audio.clip = _clips[clip].m_audio;
            _audio.Play();
            _tmp.text = _clips[clip].m_text;
            _isPlaying = true;
        }

        #endregion


        #region Privates

        [Header ("-- Audio --")]
        [SerializeField] private List<DialogueScriptableObject> _clips;
        [SerializeField] private AudioSource _audio;

        [Header("-- Text --")]
        [SerializeField] private TMP_Text _tmp;

        [Header("-- Other Parameters (optional) --")]
        [SerializeField] private List<OnTriggerAudioList> _sharedIndex;
        [SerializeField] private GameObject _toActivate;
        [SerializeField] private GameObject _toDeactivate;

        [HideInInspector] public int m_clipIndex;
        private bool _wasPlayed;
        private bool _startedPlaying;
        private bool _isPlaying;
        private float _audioLength;

        #endregion
    }
}
