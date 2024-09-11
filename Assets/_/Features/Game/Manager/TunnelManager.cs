using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class TunnelManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _counterBlackboard.SetValue<int>("TunnelCount", 0);
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.GetMask("Player")) return;

            if (_counterBlackboard.GetValue<int>("TunnelCount") < _audioList.Count)
            {
                for (int i = 0; i < _audioList.Count; i++)
                    if (i == _counterBlackboard.GetValue<int>("TunnelCount"))
                        _audioList[i].Play();
            }
            else
            {
                int rand = Random.Range(0, _audioListRandom.Count);
                _audioList[rand].Play();
            }

            IncrementTunnelCount();
        }

        #endregion

        #region Methods

        public void IncrementTunnelCount() =>
            _counterBlackboard.SetValue<int>("TunnelCount", _counterBlackboard.GetValue<int>("TunnelCount") + 1);

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _counterBlackboard;

        [Title("Audios")]
        [SerializeField]
        private List<AudioSource> _audioList;
        [SerializeField]
        private List<AudioSource> _audioListRandom;

        #endregion
    }
}
