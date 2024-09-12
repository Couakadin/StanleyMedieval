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

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            _animator.SetBool("Pull", true);
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

        [SerializeField]
        private GameObject _bridgeToInteract;

        private Animator _animator;
        private Animator _bridgeAnimator;

        #endregion
    }
}
