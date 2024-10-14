using Data.Runtime;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class SuccessBridge : SuccessAbstract
    {

        #region Unity

        private void Awake()
        {
            _bridgeAnimator = GetComponent<Animator>();
        }

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

            _bridgeAnimator.SetBool("Fall", true);
            _bridgeColliderUp.enabled = false;
            _bridgeColliderDown.enabled = true;
        }

        protected override void OnFailure()
        {

        }

        #endregion


        #region Privates

        private Animator _bridgeAnimator;

        [SerializeField] private GameObject _text;
        [SerializeField] private AudioSource _audioSuccess;
        [SerializeField] private Collider _bridgeColliderUp;
        [SerializeField] private Collider _bridgeColliderDown;

        [Header("-- Text --")]
        [SerializeField] private DialogueScriptableObject _clip;
        [SerializeField] private TMP_Text _tmp;

        #endregion
    }
}
