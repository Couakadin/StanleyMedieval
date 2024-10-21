using Data.Runtime;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class SuccessGuard : SuccessAbstract
    {
        #region Publics

        #endregion

        #region Unity

        protected void Update()
        {
            if (IsPlayerNear())
            {
                _text.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                    ArchetypeCheck();
            }
            else _text.SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 16)
            {
                OnSuccess();
            }
        }

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            _audioSuccess.clip = _clip.m_audio;
            _audioSuccess.Play();
            _tmp.GetComponent<TextCleaner>().m_resetTimer = _clip.m_audio.length + 0.5f;
            _tmp.text = _clip.m_text;
            gameObject.SetActive(false);
        }

        protected override void OnFailure()
        {
            _guardManager.IncrementGuardCount();
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        private GuardManager _guardManager;

        [SerializeField]
        private GameObject _text;
        [SerializeField]
        private AudioSource _audioSuccess;

        [Header("-- Text --")]
        [SerializeField] private DialogueScriptableObject _clip;
        [SerializeField] private TMP_Text _tmp;

        #endregion
    }
}
