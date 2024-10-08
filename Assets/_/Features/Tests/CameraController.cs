using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;
    public float mouseSensitivity = 2f;

    void Start()
    {
        // ------------------------- Hiding the Cursor

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // ------------------------------------------------------- We define our mouse movement values

        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // --------------------------------------------- Rotate the Camera around its local X axis

        _cameraVerticalRotation -= inputY;
        print(_cameraVerticalRotation);
        _cameraVerticalRotation = Mathf.Clamp(_cameraVerticalRotation, -80f, 80f);
        transform.localEulerAngles = Vector3.right * _cameraVerticalRotation;


        // -------------------------------- Rotate the Player Object and the Camera around its Y axis

        player.Rotate(Vector3.up * inputX);
    }



    private float _cameraVerticalRotation = 1f;
}
