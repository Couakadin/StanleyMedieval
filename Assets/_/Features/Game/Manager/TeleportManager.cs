using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class TeleportManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _counterBlackboard.SetValue<int>("TeleportCount", 0);
            _audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;
            
            if (_counterBlackboard.GetValue<int>("TeleportCount") < _audioList.Count)
            {
                for (int i = 0; i < _audioList.Count; i++)
                    if (i == _counterBlackboard.GetValue<int>("TeleportCount"))
                    {
                        _audioSource.clip = _audioList[i];
                        _audioSource.Play();
                    }
            }
            else
                _wallToEnable.SetActive(true);

            _playerController?.GoToThisPosition(_positionToTeleport.transform.position);

            IncrementTeleportCount();
        }

        #endregion

        #region Methods

        public void IncrementTeleportCount() =>
            _counterBlackboard.SetValue<int>("TeleportCount", _counterBlackboard.GetValue<int>("TeleportCount") + 1);

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _counterBlackboard;

        [Title("GameObjects")]
        [SerializeField] 
        private PlayerController _playerController;
        [SerializeField]
        private GameObject _wallToEnable;
        [SerializeField]
        private GameObject _positionToTeleport;

        [Title("Audios")]
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private List<AudioClip> _audioList;

        #endregion
    }
}
