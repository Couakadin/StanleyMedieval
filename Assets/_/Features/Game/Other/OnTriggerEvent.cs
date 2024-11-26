using Data.Runtime;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerEvent : MonoBehaviour
    {
        #region Unity API

        private void Update()
        {
            if (_countingDownForEvent)
            {
                _eventTimer-= Time.deltaTime;

                if (_eventTimer < 0 && !_timerEnded)
                { 
                    _eventAtTimerEnd.Raise(); 
                    _timerEnded = true;
                }
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3 && !_wasCalled)
            {
                if (_eventAtStart != null)
                    _eventAtStart.Raise();

                if (_eventAtTimerEnd != null)
                { 
                    _countingDownForEvent = true;
                    if (_fxReader != null)
                        _fxReader.AudioPlay(_audioClip);
                }

                _wasCalled = true;
            }
        }
        #endregion

        #region Private And Protected

        [Header ("-- Events --")]
        [SerializeField] private VoidScriptableEvent _eventAtStart;
        [SerializeField] private VoidScriptableEvent _eventAtTimerEnd;
        [SerializeField] private float _eventTimer;

        [SerializeField] private FXReader _fxReader;
        [SerializeField] private AudioClip _audioClip;

        private bool _wasCalled = false;
        private bool _timerEnded = false;
        private bool _countingDownForEvent = false;

        #endregion
    }
}
