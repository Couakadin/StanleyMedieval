using Data.Runtime;
using Sirenix.OdinInspector;
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

            
            if (Physics.Raycast(ray, out RaycastHit hit2, _distanceInteract, _pickableLayer))
            {
                _textPickup.SetActive(true);
                _hitObject = hit2.collider.gameObject;

                if (_hitObject.TryGetComponent<Rigidbody>(out _hitRigidbody) && IsInteracting())
                {
                    Pickable pickedItem = _hitObject.GetComponent<Pickable>();
                    if (pickedItem.m_sharedAudio.Count > 0)
                    {
                        _audioReader.AudioPlay(pickedItem.m_pickupDialogue[pickedItem._clipIndex]);

                        foreach (Pickable otherAudio in pickedItem.m_sharedAudio)
                            otherAudio._clipIndex += 1;
                    }
                    else
                        _audioReader.AudioSet(pickedItem.m_pickupDialogue);

                    _itemBlackboard.SetValue<ItemData>(pickedItem.m_itemData.m_name, pickedItem.m_itemData);
                    _playerInventory.InventoryUpdate();
                    _hitObject.SetActive(false);
                    _textPickup.SetActive(false);
                }
            }
            else if (Physics.Raycast(ray, out RaycastHit hit, _distanceInteract, _interactableLayer))
            {
                _hitObject = hit.collider.gameObject;

                if (_hitObject.GetComponent<Interactable>() == null)
                {
                    _textInteract.SetActive(true);
                }
                else
                {
                    foreach (ItemData item in _hitObject.GetComponent<Interactable>().m_itemRequired)
                    {
                        if (_itemBlackboard.GetValue<ItemData>("ActiveItem") == item)
                            _textInteract.SetActive(true);
                    }
                }
            }
            else 
            { 
                _textInteract.SetActive(false); 
                _textPickup.SetActive(false);
            }
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
        [SerializeField] private Blackboard _playerBlackboard;
        [SerializeField] private Blackboard _audioBlackboard;
        [SerializeField] private Blackboard _itemBlackboard;

        [Title("Inputs")]
        [SerializeField]
        private InputAction _interactAction;

        [Title("Audios")]
        [SerializeField]
        private AudioReader _audioReader;

        [Title("Gameobjects")]
        [SerializeField]
        private GameObject _textInteract;
        [SerializeField]
        private GameObject _textPickup;

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
