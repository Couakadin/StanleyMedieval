using UnityEngine;

namespace Game.Runtime
{
    public class PlayerCamera : MonoBehaviour
    {
        #region UNITY API

        void Awake()
        {
            // ------------------------- Hiding the Cursor

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        void Update()
        {
            if (!_cameraFrozen)
            {
                // ------------------------------------------------------- We define our mouse movement values

                float inputX = Input.GetAxis("Mouse X") * _mouseSensitivity;
                float inputY = Input.GetAxis("Mouse Y") * _mouseSensitivity;

                // --------------------------------------------- Rotate the Camera around its local X axis

                _cameraVerticalRotation -= inputY;
                _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -80f, 80f);
                transform.localEulerAngles = Vector3.right * _cameraVerticalRotation;


                // -------------------------------- Rotate the Player Object and the Camera around its Y axis

                _player.Rotate(Vector3.up * inputX);
            }
        }

        #endregion

        #region METHODS

        public void FreezeCamera()
        {
            _cameraFrozen = true;
        }
        public void UnfreezeCamera()
        {
            _cameraFrozen = false;
        }

        #endregion

        #region PRIVATE AND PROTECTED

        [Header("-- Player Ref --")]
        [SerializeField] private Transform _player;

        [Header("-- Camera Settings --")]
        [SerializeField] private float _mouseSensitivity = 1f;
        [SerializeField] private float _cameraVerticalRotation = 1f;

        private bool _cameraFrozen;

        #endregion
    }
}
