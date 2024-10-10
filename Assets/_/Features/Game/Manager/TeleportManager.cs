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

        private void Awake()
        {
            _counterBlackboard.SetValue<int>("TeleportCount", 0);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) return;

            if (_counterBlackboard.GetValue<int>("TeleportCount") < _audioList.Count - 1)
            {
                for (int i = 0; i < _audioList.Count; i++)
                    if (i == _counterBlackboard.GetValue<int>("TeleportCount"))
                    {
                        _tmp.GetComponent<TextCleaner>().m_resetTimer = _audioList[i].m_audio.length + 0.5f;
                        _audioSource.clip = _audioList[i].m_audio;
                        _audioSource.Play();
                        _tmp.text = _audioList[i].m_text;
                    }
            }
            else
            {
                _wallToEnable.SetActive(true);
                _tmp.GetComponent<TextCleaner>().m_resetTimer = _audioList[_audioList.Count - 1].m_audio.length + 0.5f;
                _audioSource.clip = _audioList[_audioList.Count - 1].m_audio;
                _audioSource.Play();
                _tmp.text = _audioList[_audioList.Count - 1].m_text;
            }

                _playerController?.GoToThisPosition(_positionToTeleport.transform.position);

            IncrementTeleportCount();
        }

        #endregion

        #region Methods

        public void IncrementTeleportCount() =>
            _counterBlackboard.SetValue<int>("TeleportCount", _counterBlackboard.GetValue<int>("TeleportCount") + 1);

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
        private List<DialogueScriptableObject> _audioList;

        [Header("-- Text --")]
        [SerializeField] private TMP_Text _tmp;

        #endregion
    }
}
