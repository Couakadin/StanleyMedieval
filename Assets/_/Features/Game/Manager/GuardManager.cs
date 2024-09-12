using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class GuardManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _counterBlackboard.SetValue<int>("GuardCount", 0);
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                OnGuardTrigger();
        }

        #endregion

        #region Methods

        public void OnGuardTrigger()
        {
            if (_counterBlackboard.GetValue<int>("GuardCount") < _audioList.Count)
            {
                for (int i = 0; i < _audioList.Count; i++)
                    if (i == _counterBlackboard.GetValue<int>("GuardCount"))
                    {
                        _audioSource.clip = _audioList[i];
                        _audioSource.Play();
                    }
            }
            else
            {
                _audioRemoveGuard.Play();
                _guardGameObject.SetActive(false);
            }
        }

        public void IncrementGuardCount() =>
            _counterBlackboard.SetValue<int>("GuardCount", _counterBlackboard.GetValue<int>("GuardCount") + 1);

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _counterBlackboard;

        [Title("Guard to interact")]
        [SerializeField]
        private GameObject _guardGameObject;

        [Title("Audios")]
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private AudioSource _audioRemoveGuard;
        [SerializeField]
        private List<AudioClip> _audioList;

        #endregion
    }
}
