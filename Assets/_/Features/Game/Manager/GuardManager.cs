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
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.GetMask("Player"))
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
                        _audioList[i].Play();
            }
            else
            {
                _audioRemoveGuard.Play();
                _guardGameObject.SetActive(false);
            }

            IncrementGuardCount();
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
        private AudioSource _audioRemoveGuard;
        [SerializeField]
        private List<AudioSource> _audioList;

        #endregion
    }
}
