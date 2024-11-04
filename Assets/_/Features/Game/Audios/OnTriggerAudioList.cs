using Data.Runtime;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerAudioList : MonoBehaviour
    {
        #region Unity

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3 && !_wasPlayed)
            { 
                AudioTypeCheck(other.gameObject);
                if (_toDeactivate != null)
                    _toDeactivate.SetActive(false);

                if (_toActivate != null)
                    _toActivate.SetActive(true);

                gameObject.GetComponent<OnTriggerAudioList>().enabled = false;
                _wasPlayed = true;
            }

        }

        #endregion


        #region Methods

        public void AudioTypeCheck(GameObject go)
        {
            if (go.layer == LayerMask.NameToLayer("Player") && _sharedIndex.Count == 0)
            {
                _audioReader.AudioSet(_clips);
            }
            else if (go.layer == LayerMask.NameToLayer("Player") && _sharedIndex.Count > 0)
            {
                _audioReader.AudioPlay(_clips[_clipIndex]);
                _clipIndex += 1;

                foreach (OnTriggerAudioList otherAudio in _sharedIndex)
                    otherAudio._clipIndex = _clipIndex;

                gameObject.SetActive(false);
            }
        }

        #endregion


        #region Privates

        [Header ("-- Audio --")]
        [SerializeField] private List<DialogueScriptableObject> _clips;
        [SerializeField] private AudioReader _audioReader;

        [Header("-- Other Parameters (optional) --")]
        [Description ("Other Items that share the same clip Index")]
        [SerializeField] private List<OnTriggerAudioList> _sharedIndex;
        [SerializeField] private GameObject _toActivate;
        [SerializeField] private GameObject _toDeactivate;

        [HideInInspector] public int _clipIndex;
        private bool _wasPlayed;

        #endregion
    }
}
