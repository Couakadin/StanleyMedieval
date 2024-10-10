using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class DeathManager : MonoBehaviour
    {
        #region Unity

        private void Awake()
        {
            _playerBlackboard.SetValue<int>("DeadCount", 0);
        }

        #endregion

        #region Methods

        public void OnResetGame()
        {
            if (_playerBlackboard.GetValue<int>("DeadCount") < _audioList.Count)
            {
                for (int i = 0; i < _audioList.Count; i++)
                    if (i == _playerBlackboard.GetValue<int>("DeadCount"))
                    {
                        _tmp.GetComponent<TextCleaner>().m_resetTimer = _audioList[i].m_audio.length + 0.5f;
                        _audioSource.clip = _audioList[i].m_audio;
                        _audioSource.Play();
                        _tmp.text = _audioList[i].m_text;
                    }
            }
            else
            {
                int rand = Random.Range(1, _audioListRandom.Count);
                _tmp.GetComponent<TextCleaner>().m_resetTimer = _audioList[rand].m_audio.length + 0.5f;
                _audioSource.clip = _audioListRandom[rand].m_audio;
                _audioSource.Play();
                _tmp.text = _audioList[rand].m_text;
            }

            _resetGameEvent.Raise();
            _guardEvent.Raise();
            _onAudioScriptEvent.Raise();
            _DeadCounterEvent.Raise();

            _bridgeAnimator.SetBool("Fall", false);
            _wallAnimator.SetBool("Fall", false);
            _leverAnimator.SetBool("Pull", false);
            _canvasArchetype.alpha = 1;
        }

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _playerBlackboard;

        [Title("Events")]
        [SerializeField]
        private VoidScriptableEvent _resetGameEvent;
        [SerializeField]
        private VoidScriptableEvent _guardEvent;
        [SerializeField]
        private VoidScriptableEvent _DeadCounterEvent;
        [SerializeField]
        private VoidScriptableEvent _onAudioScriptEvent;

        [Title("Components")]
        [SerializeField]
        private CanvasGroup _canvasArchetype;

        [Title("Audios")]
        [SerializeField]
        private AudioSource _audioSource;
        [SerializeField]
        private List<DialogueScriptableObject> _audioList;
        [SerializeField]
        private List<DialogueScriptableObject> _audioListRandom;

        [Title("TextMeshPro")]
        [SerializeField] private TMP_Text _tmp;

        [Title("Animators")]
        [SerializeField]
        private Animator _bridgeAnimator;
        [SerializeField]
        private Animator _leverAnimator;
        [SerializeField]
        private Animator _wallAnimator;

        #endregion
    }
}
