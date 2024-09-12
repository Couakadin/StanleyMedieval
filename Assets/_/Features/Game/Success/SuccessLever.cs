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
        }

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            _animator.SetBool("Pull", true);
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

        #endregion
    }
}
