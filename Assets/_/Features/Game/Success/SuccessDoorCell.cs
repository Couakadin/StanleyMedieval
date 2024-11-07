using Data.Runtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class SuccessDoorCell : SuccessAbstract
    {
        #region Unity

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (IsPlayerNear())
            {
                if (_itemBlackboard.GetValue<ItemData>("ActiveItem") == _itemRequired && !_isOpen)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        OnSuccess();
                        _rb.isKinematic = false;
                        _isOpen = true;
                        _itemBlackboard.RemoveKey("ActiveItem");
                        _itemBlackboard.RemoveKey(_itemRequired.m_name);
                        _inventoryUpdateEvent.Raise();
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 16 && !_isOpen)
            {
                OnFailure();
                _isOpen = true;
                _rb.isKinematic = false;
            }
        }

        #endregion


        #region Methods

        public void OnSuccess()
        {
            _audioReader.AudioSet(_clipSuccess);
        }

        public void OnFailure()
        {

            _audioReader.AudioSet(_clipFail);
            _cameraShakeEvent.Raise();

            if (_toActivate.Count > 0)
            {
                for (int i = 0; i < _toActivate.Count; i++)
                {
                    _toActivate[i].SetActive(true);
                }
            }
            if (_toDeactivate.Count > 0)
            {
                for (int i = 0; i < _toDeactivate.Count; i++)
                {
                    _toDeactivate[i].SetActive(false);
                }
            }
        }

        #endregion


        #region Privates

        private bool _isOpen;

        [SerializeField] private ItemData _itemRequired;
        [SerializeField] private AudioReader _audioReader;

        [SerializeField] private GameObject _text;
        [Header("-- Audio --")]
        [SerializeField] private List<DialogueScriptableObject> _clipSuccess;
        [SerializeField] private List<DialogueScriptableObject> _clipFail;

        [Header("-- Refs --")]
        [SerializeField] private Interactable _interactable;
        [SerializeField] private List<GameObject> _toActivate;
        [SerializeField] private List<GameObject> _toDeactivate;
        [SerializeField] private Blackboard _itemBlackboard;
        [SerializeField] private VoidScriptableEvent _inventoryUpdateEvent;
        [SerializeField] private VoidScriptableEvent _cameraShakeEvent;
        private Rigidbody _rb;

        #endregion
    }
}