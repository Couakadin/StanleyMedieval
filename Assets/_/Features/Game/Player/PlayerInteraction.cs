using Data.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace Game.Runtime
{
    public class PlayerInteraction : MonoBehaviour
    {

        #region Unity

        private void Awake()
        {
            _camera = Camera.main;
            _cameraTransform = Camera.main.transform;
            _mouseCurrentPosition = Mouse.current.position;

            _holdDistance = _playerBlackboard.GetValue<float>("HoldDistance");
            _distanceInteract = _playerBlackboard.GetValue<float>("DistanceInteract");
            _interactableLayer = LayerMask.GetMask("Interactable");
            _pickableLayer = LayerMask.GetMask("Pickable");
            _uiLayer = LayerMask.GetMask("InteractUI");
            _playerInventory = GetComponent<PlayerInventory>();


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
            else if (Physics.Raycast(ray, out RaycastHit hit2, _distanceInteract, _pickableLayer))
            {
                _textInteract.SetActive(true);
                _hitObject = hit2.collider.gameObject;

                if (_hitObject.TryGetComponent<Rigidbody>(out _hitRigidbody) && IsInteracting())
                {
                    //Play(_hitObject?.GetComponent<AudioSource>(), _audioBlackboard.GetValue<AudioClip>("KeyDrop"));
                    Play(_audioManager, _hitObject.GetComponent<Pickable>().m_itemData.m_pickupDialogue.m_audio);

                    _tmp.GetComponent<TextCleaner>().m_resetTimer = _hitObject.GetComponent<Pickable>().m_itemData.m_pickupDialogue.m_audio.length;
                    _tmp.text = _hitObject.GetComponent<Pickable>().m_itemData.m_pickupDialogue.m_text;

                    _playerInventory.m_items.Add(_hitObject.GetComponent<Pickable>().m_itemData);
                    _hitObject.SetActive(false);
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
        [SerializeField]
        private TMP_Text _tmp;

        [Title("Gameobjects")]
        [SerializeField]
        private GameObject _textInteract;

        [Title("Privates")]
        private Camera _camera;
        private Transform _cameraTransform;
        private LayerMask _interactableLayer;
        private LayerMask _pickableLayer;
        private LayerMask _uiLayer;
        private Vector2Control _mouseCurrentPosition;
        private GameObject _hitObject;
        private Rigidbody _hitRigidbody;
        private PlayerInventory _playerInventory;

        private float _holdDistance;
        private float _distanceInteract;

        private bool _isHoldingObject;

        #endregion
    }
}
