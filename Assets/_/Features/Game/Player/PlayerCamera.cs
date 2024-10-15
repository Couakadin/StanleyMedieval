using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerCamera : MonoBehaviour
    {
        public Transform m_player;
        public float m_mouseSensitivity = 1f;

        void Start()
        {
            // ------------------------- Hiding the Cursor

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void Update()
        {
            // ------------------------------------------------------- We define our mouse movement values

            float inputX = Input.GetAxis("Mouse X") * m_mouseSensitivity;
            float inputY = Input.GetAxis("Mouse Y") * m_mouseSensitivity;

            // --------------------------------------------- Rotate the Camera around its local X axis

            _cameraVerticalRotation -= inputY;
            _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -80f, 80f);
            transform.localEulerAngles = Vector3.right * _cameraVerticalRotation;


            // -------------------------------- Rotate the Player Object and the Camera around its Y axis

            m_player.Rotate(Vector3.up * inputX);
        }

        private float _cameraVerticalRotation = 1f;
    }
}
