using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class TunnelManager : MonoBehaviour
    {

        #region Unity

        private void Awake()
        {
            _counterBlackboard.SetValue<int>("TunnelCount", 0);
            _audioSource = GetComponent<AudioSource>();
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

            if (_counterBlackboard.GetValue<int>("TunnelCount") < _audioList.Count)
            {
                for (int i = 0; i < _audioList.Count; i++)
                    if (i == _counterBlackboard.GetValue<int>("TunnelCount"))
                    {
                        _audioSource.clip = _audioList[i];
                        _audioSource.Play();
                    }

            }
            else
            {
                int rand = Random.Range(0, _audioListRandom.Count);
                _audioSource.clip = _audioList[rand];
                _audioSource.Play();
            }

            IncrementTunnelCount();
        }

        #endregion

        #region Methods

        public void IncrementTunnelCount() =>
            _counterBlackboard.SetValue<int>("TunnelCount", _counterBlackboard.GetValue<int>("TunnelCount") + 1);

        #endregion


        #region Privates

        [Title("Data")]
        [SerializeField] private Blackboard _counterBlackboard;

        [Title("Audios")]
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private List<AudioClip> _audioList;
        [SerializeField] private List<AudioClip> _audioListRandom;

        #endregion
    }
}
