using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Game.Runtime
{
    public class OnClickAudio : MonoBehaviour
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
            _interactableLayer = LayerMask.GetMask("Audio");
            _audio.playOnAwake = false;
        }

        private void Update()
        {
            if (Input.GetMouseButton(0)) TryInteract();
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
                _hitObject = hit.collider.gameObject;
                if (_hitObject.TryGetComponent<AudioSource>(out _audio) && !_wasPlayed)
                {
                   _audio.Play();
                    _wasPlayed = true;
                }
            }
        }

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _playerBlackboard;

        [Title("Privates")]
        private Camera _camera;
        private Transform _cameraTransform;
        private LayerMask _interactableLayer;
        private Vector2Control _mouseCurrentPosition;
        private GameObject _hitObject;
        private AudioSource _audio;

        private bool _wasPlayed;

        private float _holdDistance;
        private float _distanceInteract;

        #endregion
    }
}
