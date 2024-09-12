using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Game.Runtime
{
    public class PlayerInteraction : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _camera = Camera.main;
            _cameraTransform = Camera.main.transform;
            _mouseCurrentPosition = Mouse.current.position;

            _holdDistance = _playerBlackboard.GetValue<float>("HoldDistance");
            _distanceInteract = _playerBlackboard.GetValue<float>("DistanceInteract");
            _interactableLayer = LayerMask.GetMask("Interactable");
        }

        private void OnEnable() => _interactAction.Enable();

        private void OnDisable() => _interactAction.Disable();

        private void Update()
        {
            if (_isHoldingObject)
            {
                HoldObject();
            }
            else
            {
                TryInteract();
            }

            if (IsRelease() && _isHoldingObject)
            {
                ReleaseObject();
            }
        }

        #endregion

        #region Methods

        #endregion

        #region Utils

        private void TryInteract()
        {
            Ray ray = _camera.ScreenPointToRay(_mouseCurrentPosition.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, _distanceInteract, _interactableLayer))
            {
                _textInteract.SetActive(true);
                _hitObject = hit.collider.gameObject;
                if (_hitObject.TryGetComponent<Rigidbody>(out _hitRigidbody) && IsInteracting())
                {
                    Play(_hitObject?.GetComponent<AudioSource>(), _audioBlackboard.GetValue<AudioClip>("KeyDrop"));
                    _isHoldingObject = true;
                    _hitRigidbody.isKinematic = false;
                    _textInteract.SetActive(false);
                }
            }
            else _textInteract.SetActive(false);
        }

        private void HoldObject()
        {
            Ray ray = _camera.ScreenPointToRay(_mouseCurrentPosition.ReadValue());
            Vector3 targetPosition = ray.GetPoint(_distanceInteract);
            Vector3 direction = targetPosition - _hitObject.transform.position;

            _hitRigidbody.velocity = direction * 10f;
            _hitRigidbody.MovePosition(targetPosition);
        }

        private void ReleaseObject()
        {
            Play(_hitObject.GetComponent<AudioSource>(), _audioBlackboard.GetValue<AudioClip>("KeyGrab"));
            _isHoldingObject = false;
            _hitRigidbody.isKinematic = false;

            //_hitRigidbody.AddForce(_camera.transform.forward * 5f, ForceMode.Impulse);
        }

        private void Play(AudioSource audioSource, AudioClip clipToPlay)
        {
            if (!audioSource.gameObject.activeSelf) return;
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }

        private bool IsInteracting() => _interactAction.triggered;
        private bool IsRelease() => _interactAction.WasReleasedThisFrame();

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _playerBlackboard;
        [SerializeField]
        private Blackboard _audioBlackboard;

        [Title("Inputs")]
        [SerializeField]
        private InputAction _interactAction;

        [Title("Audios")]
        [SerializeField]
        private AudioSource _audioManager;

        [Title("Gameobjects")]
        [SerializeField]
        private GameObject _textInteract;

        [Title("Privates")]
        private Camera _camera;
        private Transform _cameraTransform;
        private LayerMask _interactableLayer;
        private Vector2Control _mouseCurrentPosition;
        private GameObject _hitObject;
        private Rigidbody _hitRigidbody;

        private float _holdDistance;
        private float _distanceInteract;

        private bool _isHoldingObject;

        #endregion
    }
}
