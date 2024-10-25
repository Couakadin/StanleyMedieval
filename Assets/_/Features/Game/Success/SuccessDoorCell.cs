using Data.Runtime;
using System.Collections.Generic;
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 3 && !_isOpen)
            {
                foreach (ItemData item in collision.gameObject.GetComponent<PlayerInventory>().m_items)
                {
                    if (item == _requiredItem)
                    {
                        OnSuccess();
                        _rb.isKinematic = false;
                        _isOpen = true;
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

            if (_toActivate != null)
                _toActivate.SetActive(true);
            if (_toDeactivate != null)
                _toDeactivate.SetActive(false);
        }

        #endregion


        #region Privates

        private bool _isOpen;

        [SerializeField] private AudioReader _audioReader;

        [Header("-- Text --")]
        [SerializeField] private List<DialogueScriptableObject> _clipSuccess;
        [SerializeField] private List<DialogueScriptableObject> _clipFail;

        [Header("-- Refs --")]
        [SerializeField] private ItemData _requiredItem;
        [SerializeField] private GameObject _toActivate;
        [SerializeField] private GameObject _toDeactivate;
        private Rigidbody _rb;

        #endregion
    }
}