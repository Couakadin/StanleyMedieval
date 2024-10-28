using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerManager : MonoBehaviour
    {

        #region Unity

        private void Start()
        {
            _playerBlackboard.SetValue<bool>("IsDead", false);
            _itemBlackboard.Clear();
        }

        #endregion

        #region Methods
        public void IsDead(bool dead) => _playerBlackboard.SetValue<bool>("IsDead", dead);

        public void GetDeadCount() => _playerBlackboard.GetValue<int>("DeadCount");

        public void IncrementDeadCount() =>
            _playerBlackboard.SetValue<int>("DeadCount", _playerBlackboard.GetValue<int>("DeadCount") + 1);

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField] private Blackboard _playerBlackboard;
        [SerializeField] private Blackboard _itemBlackboard;

        #endregion
    }
}
