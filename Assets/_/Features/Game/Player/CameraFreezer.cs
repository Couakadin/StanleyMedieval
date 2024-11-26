using Data.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class CameraFreezer : MonoBehaviour
    {
        private void Update()
        {
            if (_camFrozen && _frozenDuration > -1)
            {
                _frozenDuration -= Time.deltaTime;

                if (_frozenDuration < 0)
                {
                    foreach (PlayerCamera cam in _playerCameras)
                        cam.UnfreezeCamera();
                    if (_eventAtEnd != null)
                    {
                        _eventAtEnd.Raise();
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_camFrozen)
            {
                FreezePlayerCamera();
                _camFrozen = true;
            }
        }

        public void FreezePlayerCamera()
        {
            foreach(PlayerCamera cam in _playerCameras)
                cam.FreezeCamera();

            if (_eventAtStart != null)
            {
                _eventAtStart.Raise();
            }
        }

        [SerializeField] private List<PlayerCamera> _playerCameras;
        [SerializeField] private float _frozenDuration;
        [SerializeField] private VoidScriptableEvent _eventAtEnd;
        [SerializeField] private VoidScriptableEvent _eventAtStart;

        private bool _camFrozen;
    }
}
