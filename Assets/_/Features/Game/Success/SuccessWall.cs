using UnityEngine;

namespace Game.Runtime
{
    [RequireComponent(typeof(Animator), typeof(BoxCollider))]
    public class SuccessWall : SuccessAbstract
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            _animator.SetBool("Fall", true);
            _boxCollider.enabled = false;
        }

        protected override void OnFailure()
        {
            _deathEvent.Raise();
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        private Animator _animator;
        private BoxCollider _boxCollider;

        #endregion
    }
}
