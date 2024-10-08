using UnityEngine;

namespace Game.Runtime
{
    public class SuccessLever : SuccessAbstract
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _bridgeAnimator = _bridgeToInteract.GetComponent<Animator>();
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

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            _audioSuccess.Play();
            _animator.SetBool("Pull", true);
            _bridgeAnimator.SetBool("Fall", true);
        }

        protected override void OnFailure()
        {
            
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        private GameObject _bridgeToInteract;

        private Animator _animator;
        private Animator _bridgeAnimator;

        [SerializeField]
        private GameObject _text;
        [SerializeField]
        private AudioSource _audioSuccess;

        #endregion
    }
}
