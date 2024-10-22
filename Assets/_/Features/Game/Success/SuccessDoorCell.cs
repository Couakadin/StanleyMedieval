using Data.Runtime;
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

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 3 && !_isOpen)
            {
                foreach (ItemData item in collision.gameObject.GetComponent<PlayerInventory>().m_items)
                {
                    if (item == _requiredItem)
                    {
                        OnSuccess();
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 16 && !_isOpen)
            {
                OnFailure();
            }
        }

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            _rb.isKinematic = false;
            _isOpen = true;

            if (_clipSuccess.m_audio != null)
            { 
                _tmp.GetComponent<TextCleaner>().m_resetTimer = _clipSuccess.m_audio.length + 0.5f;
                _audioSource.clip = _clipSuccess.m_audio;
                _audioSource.Play();
            }
            else
                _tmp.GetComponent<TextCleaner>().m_resetTimer = 7.5f;

            _tmp.text = _clipSuccess.m_text;

            _rb.isKinematic = false;
        }

        protected override void OnFailure()
        {
            _rb.isKinematic = false;
            _isOpen = true;

            if (_clipFail.m_audio != null)
            { 
                _tmp.GetComponent<TextCleaner>().m_resetTimer = _clipFail.m_audio.length + 0.5f;
                _audioSource.clip = _clipFail.m_audio;
                _audioSource.Play();
            }
            else
                _tmp.GetComponent<TextCleaner>().m_resetTimer = 7.5f;
            _tmp.text = _clipFail.m_text;
        }

        #endregion

        #region Privates

        private bool _isOpen;

        [SerializeField] private AudioSource _audioSource;

        [Header("-- Text --")]
        [SerializeField] private DialogueScriptableObject _clipSuccess;
        [SerializeField] private DialogueScriptableObject _clipFail;
        [SerializeField] private TMP_Text _tmp;

        [Header("-- Refs --")]
        [SerializeField] private ItemData _requiredItem;
        private Rigidbody _rb;

        #endregion
    }
}
