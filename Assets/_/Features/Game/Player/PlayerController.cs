using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
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
            _audioSource = GetComponent<AudioSource>();
            _initialStandHeight = 2f;
            _standHeight = _initialStandHeight;
            _crouchHeight = .9f;

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

        private void Start() =>
            _playerBlackboard.SetValue<Vector3>("InitialPosition", transform.position);

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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Water")) _isTunnel = true;
            if (_isTunnel && collision.gameObject.layer == LayerMask.NameToLayer("Ground")) _isTunnel = false;

            if (collision.gameObject.layer == LayerMask.NameToLayer("PrisonDoor"))
            {
                if (collision.gameObject.TryGetComponent<LockManager>(out LockManager lockManager))
                    if (lockManager.IsLocked())
                        Play(_audioManager, _audioBlackboard.GetValue<AudioClip>("CollisionPrisonDoorLocked"));
            }

            if (collision.gameObject.layer == LayerMask.NameToLayer("WoodDoor"))
            {
                if (collision.gameObject.TryGetComponent<LockManager>(out LockManager lockManager))
                {
                    if (lockManager.IsLocked())
                        Play(_audioManager, _audioBlackboard.GetValue<AudioClip>("CollisionWoodDoorLocked"));
                    else
                        Play(_audioManager, _audioBlackboard.GetValue<AudioClip>("CollisionWoodDoorOpen"));
                }
            }
        }

        private void FixedUpdate()
        {
            if (_playerBlackboard.GetValue<bool>("IsDead")) return;

            ViewAction();
            SpeedAction();
            if (!IsJumping() && IsGrounded()) CrouchAction();
            if (IsGrounded() && IsJumping()) JumpAction();
            MoveAction();
        }

        private void LateUpdate()
        {
            _playerBlackboard.SetValue<Vector3>("Position", transform.position);
            _playerBlackboard.SetValue<Vector3>("Forward", _viewTransform.forward);
        }

        #endregion

        #region Methods

        public void GoToInitialPosition()
        {
            if (_playerBlackboard.ContainsKey("InitialPosition"))
                transform.position = _playerBlackboard.GetValue<Vector3>("InitialPosition");
        }

        public void GoToThisPosition(Vector3 position)
        {
            transform.position = position;
        }

        #endregion

        #region Utils

        private void Play(AudioSource audioSource, AudioClip clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }

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

            if (_rigidbody.velocity.magnitude > 0.5f && Time.time - _lastPlayTime > .7f && !IsJumping())
                Play();
        }

        private void JumpAction() => _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);

        private void CrouchAction()
        {
            float previousHeight = _capsuleCollider.height;

            _capsuleCollider.height = IsCrouching() ? _crouchHeight : _standHeight;
            _capsuleCollider.radius = .3f;
            float heightDifference = previousHeight - _capsuleCollider.height;
            _viewTransform.position += new Vector3(0, -heightDifference / 2, 0);
        }

        public float rotationSmoothTime = 0.1f;  // Temps pour lisser la rotation
        private Quaternion _currentRotation;
        private Quaternion _targetRotation;

        private void ViewAction()
        {
            // Récupération de la rotation de la caméra (avec Cinemachine)
            Quaternion cameraRotation = _cameraTransform.rotation;

            // Calcul des axes vers l'avant et sur les côtés en fonction de la caméra
            _forward = cameraRotation * Vector3.forward;
            _right = cameraRotation * Vector3.right;

            // Conversion de la rotation de la caméra en angles d'Euler
            Vector3 cameraAngle = cameraRotation.eulerAngles;

            // Calcul de la rotation cible du joueur sur l'axe Y (ne pas inclure X et Z)
            _targetRotation = Quaternion.Euler(0, cameraAngle.y, 0);

            // Lissage de la rotation du joueur avec Quaternion.Lerp
            _currentRotation = Quaternion.Lerp(_rigidbody.rotation, _targetRotation, Time.deltaTime / rotationSmoothTime);

            // Appliquer la rotation lissée au Rigidbody
            _rigidbody.MoveRotation(_currentRotation);
        }

        private void SpeedAction()
        {
            if (IsCrouching()) _movementSpeed = _crouchSpeed;
            else if (IsRunning()) _movementSpeed = _runSpeed;
            else _movementSpeed = _walkSpeed;
        }

        private bool IsCrouching() 
        {
            if (_crouchAction.IsPressed() || _isTunnel) return true;
            return false;
        }
        
        private bool IsJumping() => _jumpAction.IsPressed();
        
        private bool IsRunning() => _runAction.IsPressed();

        private bool IsGrounded() => _isGrounded = 
            Physics.CheckCapsule(_capsuleCollider.bounds.center,
                new Vector3(
                    _capsuleCollider.bounds.center.x, 
                    _capsuleCollider.bounds.min.y, 
                    _capsuleCollider.bounds.center.z), 0.1f, _groundLayer);

        private void Play()
        {
            _lastPlayTime = Time.time;
            AudioClip clipToPlay = _stepfoot[Random.Range(0, _stepfoot.Count)];
            _audioSource.clip = clipToPlay;
            _audioSource.Play();
        }

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _playerBlackboard;
        [SerializeField]
        private Blackboard _audioBlackboard;

        [Title("Components")]
        [SerializeField]
        private CapsuleCollider _capsuleCollider;
        [SerializeField]
        private Transform _viewTransform;

        [Title("Audios")]
        [SerializeField]
        private AudioSource _audioManager;

        [Title("Inputs")]
        [SerializeField]
        private InputAction _moveAction;
        [SerializeField]
        private InputAction _crouchAction;
        [SerializeField]
        private InputAction _jumpAction;
        [SerializeField]
        private InputAction _runAction;

        [Title("Audio")]
        [SerializeField]
        private List<AudioClip> _stepfoot;

        [Title("Privates")]
        private Vector3 _movement;
        private Rigidbody _rigidbody;
        private LayerMask _groundLayer;
        private Vector3 _forward, _right;
        private Transform _cameraTransform;
        private AudioSource _audioSource;

        private bool _isGrounded, _isTunnel;

        private float _jumpForce;
        private float _horizontal, _vertical;
        private float _crouchHeight, _standHeight, _initialStandHeight;
        private float _movementSpeed, _walkSpeed, _runSpeed, _crouchSpeed;
        private float _lastPlayTime;

        #endregion
    }
}
