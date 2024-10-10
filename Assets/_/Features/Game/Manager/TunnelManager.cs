using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
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
                        _tmp.GetComponent<TextCleaner>().m_resetTimer = _audioList[i].m_audio.length + 0.5f;
                        _audioSource.clip = _audioList[i].m_audio;
                        _audioSource.Play();
                        _tmp.text = _audioList[i].m_text;
                    }

            }
            /*else
            {
                int rand = Random.Range(0, _audioListRandom.Count);
                _tmp.GetComponent<TextCleaner>().m_resetTimer = _audioListRandom[rand].m_audio.length + 0.5f;
                _audioSource.clip = _audioListRandom[rand].m_audio;
                _audioSource.Play();
                _tmp.text = _audioListRandom[rand].m_text;
            }*/

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
        [SerializeField] private List<DialogueScriptableObject> _audioList;
        [SerializeField] private List<DialogueScriptableObject> _audioListRandom;

        [Title("TextMeshPro")]
        [SerializeField] private TMP_Text _tmp;

        #endregion
    }
}
