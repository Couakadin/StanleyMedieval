using Cinemachine;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerCameraChange : MonoBehaviour
    {
        #region Unity API

        private void Update()
        {
            if (_wasUsed && _changeDuration > -1)
            {
                _changeDuration -= Time.deltaTime;

                if (_changeDuration <= 0)
                    _newActiveCamera.Priority = -5;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3 && !_wasUsed)
            {
                ChangeCamera();
            }
        }

        #endregion

        #region Methods

        public void ChangeCamera()
        {
            _newActiveCamera.Priority = 50;
            _wasUsed = true;
        }

        #endregion

        #region Private and Protected

        [SerializeField] private CinemachineVirtualCamera _newActiveCamera;
        [SerializeField] private float _changeDuration;

        private bool _wasUsed = false;

        #endregion
    }
}
