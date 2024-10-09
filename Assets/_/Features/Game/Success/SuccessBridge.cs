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

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            _audioSuccess.Play();
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

        #endregion
    }
}
