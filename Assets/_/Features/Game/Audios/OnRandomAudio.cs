using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class OnRandomTriggerAudio : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player") && !_wasPlayed) return;

            int rand = Random.Range(0, _audioListRandom.Count);
            _audioSource.clip = _audioListRandom[rand];
            _audioSource.Play();
            _wasPlayed = true;
        }

        #endregion

        #region Methods

        public void ResetWasPlayed() => _wasPlayed = false;


        #endregion

        #region Utils

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
        private List<AudioClip> _audioListRandom;

        private bool _wasPlayed;

        #endregion
    }
}
