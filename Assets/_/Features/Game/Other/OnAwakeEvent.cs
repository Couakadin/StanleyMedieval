using Data.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class OnAwakeEvent : MonoBehaviour
    {
        #region Unity API

        private void Awake()
        {
            if (_eventAtStart != null)
                _eventAtStart.Raise();

            if (_eventAtTimerEnd != null)
                _countingDownForEvent = true;
        }
        private void Update()
        {
            if (_countingDownForEvent)
            {
                _eventTimer -= Time.deltaTime;

                if (_eventTimer < 0 && !_timerEnded)
                {
                    _eventAtTimerEnd.Raise();
                    _timerEnded = true;
                }
            }
        }

        #endregion

        #region Private And Protected

        [Header("-- Events --")]
        [SerializeField] private VoidScriptableEvent _eventAtStart;
        [SerializeField] private VoidScriptableEvent _eventAtTimerEnd;
        [SerializeField] private float _eventTimer;

        private bool _timerEnded = false;
        private bool _countingDownForEvent = false;

        #endregion
    }
}
