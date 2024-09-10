using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Runtime
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _cameraTransform = Camera.main.transform;
            _groundLayer = LayerMask.GetMask("Ground");
            _standHeight = 2f;
            _crouchHeight = 1f;

            if (_playerBlackboard.ContainsKey("WalkSpeed"))
                _walkSpeed = _playerBlackboard.GetValue<float>("WalkSpeed");
            if (_playerBlackboard.ContainsKey("RunSpeed"))
                _runSpeed = _playerBlackboard.GetValue<float>("RunSpeed");
            if (_playerBlackboard.ContainsKey("CrouchSpeed"))
                _crouchSpeed = _playerBlackboard.GetValue<float>("CrouchSpeed");
            if (_playerBlackboard.ContainsKey("JumpForce"))
                _jumpForce = _playerBlackboard.GetValue<float>("JumpForce");

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnEnable()
        {
            _runAction.Enable();
            _moveAction.Enable();
            _jumpAction.Enable();
            _crouchAction.Enable();
        }

        private void OnDisable()
        {
            _runAction.Disable();
            _moveAction.Disable();
            _jumpAction.Disable();
            _crouchAction.Disable();
        }

        private void FixedUpdate()
        {
            ViewAction();
            SpeedAction();
            CrouchAction();
            if (IsGrounded() && IsJumping()) JumpAction();
            MoveAction();
        }

        private void LateUpdate() =>
            _playerBlackboard.SetValue<Vector3>("Position", transform.position);

        #endregion

        #region Methods

        #endregion

        #region Utils

        private void MoveAction()
        {
            Vector2 position = _moveAction.ReadValue<Vector2>();

            _horizontal = position.x;
            _vertical = position.y;

            _movement = _horizontal * _right + _vertical * _forward;
            _movement.y = 0;
            _movement.Normalize();

            _rigidbody.velocity = new Vector3(
                _movement.x * _movementSpeed * Time.fixedDeltaTime,
                _rigidbody.velocity.y,
                _movement.z * _movementSpeed * Time.fixedDeltaTime);
        }

        private void JumpAction() => _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        private void CrouchAction()
        {
            float previousHeight = _capsuleCollider.height;
            _capsuleCollider.height = IsCrouching() ? _crouchHeight : _standHeight;
            float heightDifference = previousHeight - _capsuleCollider.height;
            transform.position += new Vector3(0, -heightDifference / 2, 0);
        }

        private void ViewAction()
        {
            Quaternion cameraRotation = _cameraTransform.rotation;

            _forward = cameraRotation * Vector3.forward;
            _right = cameraRotation * Vector3.right;
            Vector3 cameraAngle = cameraRotation.eulerAngles;
            
            Quaternion rotation = Quaternion.Euler(0, cameraAngle.y, 0);
            _rigidbody.MoveRotation(rotation);
        }

        private void SpeedAction()
        {
            if (IsCrouching()) _movementSpeed = _crouchSpeed;
            else if (IsRunning()) _movementSpeed = _runSpeed;
            else _movementSpeed = _walkSpeed;
        }

        private bool IsCrouching() => _crouchAction.IsPressed();
        
        private bool IsJumping() => _jumpAction.IsPressed();
        
        private bool IsRunning() => _runAction.IsPressed();

        private bool IsGrounded() => _isGrounded = 
            Physics.CheckCapsule(_capsuleCollider.bounds.center,
                new Vector3(
                    _capsuleCollider.bounds.center.x, 
                    _capsuleCollider.bounds.min.y, 
                    _capsuleCollider.bounds.center.z), 0.1f, _groundLayer);

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _playerBlackboard;

        [Title("Components")]
        [SerializeField]
        private CapsuleCollider _capsuleCollider;

        [Title("Inputs")]
        [SerializeField]
        private InputAction _moveAction;
        [SerializeField]
        private InputAction _crouchAction;
        [SerializeField]
        private InputAction _jumpAction;
        [SerializeField]
        private InputAction _runAction;

        [Title("Privates")]
        private Vector3 _movement;
        private Rigidbody _rigidbody;
        private LayerMask _groundLayer;
        private Vector3 _forward, _right;
        private Transform _cameraTransform;

        private bool _isGrounded;

        private float _jumpForce;
        private float _horizontal, _vertical;
        private float _crouchHeight, _standHeight;
        private float _movementSpeed, _walkSpeed, _runSpeed, _crouchSpeed;

        #endregion
    }
}
