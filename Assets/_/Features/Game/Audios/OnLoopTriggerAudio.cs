using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class OnLoopTriggerAudio : MonoBehaviour
    {

        #region Unity

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player") && !_wasPlayed) return;

            if (_blackboard.GetValue<int>(_index) < _audioList.Count)
            {
                for (int i = 0; i < _audioList.Count; i++)
                    if (i == _blackboard.GetValue<int>(_index))
                    {
                        _audioSource.clip = _audioList[i];
                        _audioSource.Play();
                    }
            }
        }

        #endregion

        #region Methods

        public void ResetWasPlayed() => _wasPlayed = false;

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _blackboard;
        [SerializeField]
        private string _index;

        [Title("Audios")]
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private List<AudioClip> _audioList;

        private bool _wasPlayed;

        #endregion
    }
}
