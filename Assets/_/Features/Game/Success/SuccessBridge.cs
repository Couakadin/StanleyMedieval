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

        #endregion
    }
}
