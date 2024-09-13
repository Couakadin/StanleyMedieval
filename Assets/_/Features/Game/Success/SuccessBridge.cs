using UnityEngine;

namespace Game.Runtime
{
    public class SuccessBridge : SuccessAbstract
    {
        #region Publics

        #endregion

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
            _bridgeAnimator.SetBool("Fall", true);
        }

        protected override void OnFailure()
        {
            _deathEvent.Raise();
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        private Animator _bridgeAnimator;

        [SerializeField]
        private GameObject _text;

        #endregion
    }
}
