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

                if (_eventTimer < 0)
                    _eventAtTimerEnd.Raise();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3)
            {
                if (_eventAtStart != null)
                    _eventAtStart.Raise();

                if (_eventAtTimerEnd != null) 
                    _countingDownForEvent = true;
            }
        }
        #endregion

        #region Private And Protected

        [Header ("-- Events --")]
        [SerializeField] private VoidScriptableEvent _eventAtStart;
        [SerializeField] private VoidScriptableEvent _eventAtTimerEnd;
        [SerializeField] private float _eventTimer;

        private bool _countingDownForEvent = false;

        #endregion
    }
}
