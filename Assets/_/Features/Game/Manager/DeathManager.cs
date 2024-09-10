using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Runtime
{
    public class DeathManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        #endregion

        #region Methods

        public void OnResetGame()
        {
            _resetGameEvent.Raise();
            _DeadCounterEvent.Raise();
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Events")]
        [SerializeField]
        private VoidScriptableEvent _resetGameEvent;
        [SerializeField]
        private VoidScriptableEvent _DeadCounterEvent;

        #endregion
    }
}
