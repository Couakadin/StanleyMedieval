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
            if (_opening)
            {
                _audioLength -= Time.deltaTime;

                if (_audioLength <= 0 ) 
                {
                    if (_clipIndex < _clipFail.Count && _failed)
                    {
                        OnFailure(_clipIndex);
                    }
                    else if (_clipIndex < _clipSuccess.Count && _succeeded)
                    {
                        OnSuccess(_clipIndex);
                    }
                        _rb.isKinematic = false;
                }
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 3 && !_isOpen)
            {
                foreach (ItemData item in collision.gameObject.GetComponent<PlayerInventory>().m_items)
                {
                    if (item == _requiredItem)
                    {
                        OnSuccess(_clipIndex);
                        _succeeded = true;
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
                OnFailure(_clipIndex);
                _failed = true;
                _opening = true;
                _isOpen = true;
            }
        }

        #endregion


        #region Methods

        public void OnSuccess(int i)
        {

            if (_clipSuccess[i].m_audio.length > 0)
            {
                _tmp.GetComponent<TextCleaner>().m_resetTimer = _clipSuccess[i].m_audio.length + 0.5f;
                _audioSource.clip = _clipSuccess[i].m_audio;
                _audioSource.Play();
            }
            else
            { 
                _tmp.GetComponent<TextCleaner>().m_resetTimer = 7.5f;
                _audioLength = 7.5f;
            }

            _tmp.text = _clipSuccess[i].m_text;
            _clipIndex += 1;
        }

        public void OnFailure(int i)
        {

            if (_clipFail[i].m_audio != null)
            {
                _tmp.GetComponent<TextCleaner>().m_resetTimer = _clipFail[i].m_audio.length + 0.5f;
                _audioLength = _clipFail[i].m_audio.length;
                _audioSource.clip = _clipFail[i].m_audio;
                _audioSource.Play();
            }
            else
            { 
                _tmp.GetComponent<TextCleaner>().m_resetTimer = 7.5f;
                _audioLength = 7.5f;
            }

            if (_toActivate != null)
                _toActivate.SetActive(true);
            if (_toDeactivate != null)
                _toDeactivate.SetActive(false);

            _tmp.text = _clipFail[i].m_text;
            _clipIndex += 1;
        }

        #endregion


        #region Privates

        private bool _isOpen;
        private bool _opening;
        private bool _succeeded;
        private bool _failed;
        private float _audioLength = 5;
        private int _clipIndex;

        [SerializeField] private AudioSource _audioSource;

        [Header("-- Text --")]
        [SerializeField] private List<DialogueScriptableObject> _clipSuccess;
        [SerializeField] private List<DialogueScriptableObject> _clipFail;
        [SerializeField] private TMP_Text _tmp;

        [Header("-- Refs --")]
        [SerializeField] private ItemData _requiredItem;
        [SerializeField] private GameObject _toActivate;
        [SerializeField] private GameObject _toDeactivate;
        private Rigidbody _rb;

        #endregion
    }
}