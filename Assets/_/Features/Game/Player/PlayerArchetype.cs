using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerArchetype : MonoBehaviour
    {
        #region Publics

        public enum Archetype
        {
            NONE,
            WARRIOR,
            ROGUE,
            MAGE
        }

        public Archetype m_archetype
        {
            get
            {
                return _archetype;
            }
            set
            {
                _archetype = value;
                _playerBlackboard.SetValue<Archetype>("Archetype", _archetype);
            }
        }

        #endregion

        #region Unity

        private void Start() => m_archetype = Archetype.NONE;

        #endregion

        #region Methods

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        private Blackboard _playerBlackboard;

        [Title("Privates")]
        private Archetype _archetype;

        #endregion
    }
}
