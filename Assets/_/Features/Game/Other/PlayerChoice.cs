using Data.Runtime;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerChoice : MonoBehaviour
    {
        public void StrengthUp()
        {
            _playerBlackboard.SetValue<int>("Strength", _playerBlackboard.GetValue<int>("Strength")+1);
            gameObject.SetActive(false);
        }
        public void AgilityUp()
        {
            _playerBlackboard.SetValue<int>("Agility", _playerBlackboard.GetValue<int>("Agility") + 1);
            gameObject.SetActive(false);
        }

        [SerializeField] private Blackboard _playerBlackboard;
    }

}
