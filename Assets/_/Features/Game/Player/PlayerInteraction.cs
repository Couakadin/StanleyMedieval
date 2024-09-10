using Data.Runtime;
using Sirenix.OdinInspector;
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

        private void Update() => InteractAction();

        #endregion

        #region Methods

        #endregion

        #region Utils

        private void InteractAction()
        {
            if (IsInteracting()) TryInteract();
            else DeselectInteract();
        }

        private void TryInteract()
        {
            Ray ray = _camera.ScreenPointToRay(_mouseCurrentPosition.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit, _distanceInteract, _interactableLayer))
            {
                _hitObject = hit.collider.gameObject;
                if (_hitObject.TryGetComponent<Rigidbody>(out _hitRigidbody))
                {
                    _hitRigidbody.isKinematic = true;
                    Vector3 newPosition = _cameraTransform.position + _cameraTransform.forward * _holdDistance;
                    _hitObject.transform.position = newPosition;
                }
            }
        }

        private void DeselectInteract()
        {
            if (_hitRigidbody != null)
                _hitRigidbody.isKinematic = false;

            _hitRigidbody = null;
            _hitObject = null;
        }

        private bool IsInteracting() => _interactAction.IsPressed();

        #endregion

        #region Privates

        [Title("Inputs")]
        [SerializeField]
        private Blackboard _playerBlackboard;

        [Title("Inputs")]
        [SerializeField]
        private InputAction _interactAction;

        [Title("Privates")]
        private Camera _camera;
        private Transform _cameraTransform;
        private LayerMask _interactableLayer;
        private Vector2Control _mouseCurrentPosition;
        private GameObject _hitObject;
        private Rigidbody _hitRigidbody;

        private float _holdDistance;
        private float _distanceInteract;

        #endregion
    }
}
