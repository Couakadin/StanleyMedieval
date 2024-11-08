using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class TeleportManager : MonoBehaviour
    {

        #region Unity

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

            if (_teleportCounter < _audioList.Count - 1)
            {
                for (int i = 0; i < _audioList.Count; i++)
                    if (i == _teleportCounter)
                    {
                        _audioReader.AudioPlay(_audioList[i]);
                    }
            }
            else
            {
                foreach (GameObject item in _objectsToEnable)
                    item.SetActive(true);
                //_audioReader.AudioPlay(_audioList[_audioList.Count-1]);
            }

            _playerController?.GoToThisPosition(_positionToTeleport.transform.position);
            _playerController.transform.rotation = _positionToTeleport.transform.rotation;

            IncrementTeleportCount();
        }

        #endregion

        #region Methods

        public void IncrementTeleportCount() =>
            _teleportCounter++;

        #endregion


        #region Privates

        [Title("GameObjects")]
        [SerializeField] 
        private PlayerController _playerController;
        [SerializeField]
        private List<GameObject> _objectsToEnable;
        [SerializeField]
        private GameObject _positionToTeleport;

        [Title("Audios")]
        [SerializeField] private AudioReader _audioReader;
        [SerializeField] private List<DialogueScriptableObject> _audioList;

        private int _teleportCounter;

        #endregion
    }
}
