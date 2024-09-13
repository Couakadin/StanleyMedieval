using Data.Runtime;
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

        [SerializeField]
        private GameObject _text;

        #endregion
    }
}
