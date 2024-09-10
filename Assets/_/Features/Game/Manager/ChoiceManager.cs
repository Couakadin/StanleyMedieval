using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Runtime
{
    public class ChoiceManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        #endregion

        #region Methods

        public void Choice(int val)
        {
            if (_playerBlackboard.GetValue<PlayerArchetype.Archetype>("Archetype") == (PlayerArchetype.Archetype)val)
                _successEvent.Raise();
            else _deathEvent.Raise();
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _playerBlackboard;

        [Title("Events")]
        [SerializeField]
        private VoidScriptableEvent _deathEvent;
        [SerializeField]
        private VoidScriptableEvent _successEvent;

        #endregion
    }
}
