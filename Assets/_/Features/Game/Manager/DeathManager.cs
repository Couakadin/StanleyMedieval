using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class DeathManager : MonoBehaviour
    {
        #region Publics

        #endregion

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
                        _audioList[i]?.Play();
            }
            else
            {
                int rand = Random.Range(0, _audioListRandom.Count);
                _audioList[rand]?.Play();
            }

            _resetGameEvent.Raise();
            _DeadCounterEvent.Raise();
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _playerBlackboard;

        [Title("Events")]
        [SerializeField]
        private VoidScriptableEvent _resetGameEvent;
        [SerializeField]
        private VoidScriptableEvent _DeadCounterEvent;

        [Title("Audios")]
        [SerializeField]
        private List<AudioSource> _audioList;
        [SerializeField]
        private List<AudioSource> _audioListRandom;

        #endregion
    }
}
