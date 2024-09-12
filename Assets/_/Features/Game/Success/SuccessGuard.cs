using Data.Runtime;
using UnityEngine;

namespace Game.Runtime
{
    public class SuccessGuard : SuccessAbstract
    {
        #region Publics

        #endregion

        #region Unity

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            gameObject.SetActive(false);
        }

        protected override void OnFailure()
        {
            _guardManager.IncrementGuardCount();
            _deathEvent.Raise();
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        private GuardManager _guardManager;

        #endregion
    }
}
