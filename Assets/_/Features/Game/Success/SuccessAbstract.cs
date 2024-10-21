using Data.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public abstract class SuccessAbstract : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        #endregion

        #region Methods

        #endregion

        #region Utils

        protected bool IsPlayerNear()
        {
            Vector3 playerPosition = _playerBlackboard.GetValue<Vector3>("Position");
            Vector3 directionToObject = transform.position - playerPosition;

            float distanceSqr = directionToObject.sqrMagnitude;
            if (distanceSqr > 4f * 4f) return false;

            Vector3 playerForward = _playerBlackboard.GetValue<Vector3>("Forward");

            directionToObject.Normalize();
            float dotProduct = Vector3.Dot(playerForward, directionToObject);

            //if (dotProduct > 0.6f) return true;

            return true;
        }

        protected void TextToShow() => _textCanvas.text = _textToShow;

        protected void ArchetypeCheck()
        {
            if (_playerBlackboard.GetValue<PlayerArchetype.Archetype>("Archetype") == (PlayerArchetype.Archetype)_archetypeSuccess)
                OnSuccess();
            else OnFailure();
        }

        protected void StatCheck(string stat)
        {
            if (_playerBlackboard.GetValue<int>(stat) == (int)_statSuccess)
                OnSuccess();
            else OnFailure();
        }

        protected virtual void OnSuccess() { }

        protected virtual void OnFailure() { }

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        protected Blackboard _playerBlackboard;

        [Title("Events")]
        [SerializeField]
        protected VoidScriptableEvent _deathEvent;

        [Title("Values")]
        [SerializeField]
        protected int _archetypeSuccess;
        [SerializeField]
        protected int _statSuccess;
        [SerializeField]
        protected TextMeshProUGUI _textCanvas;
        [SerializeField]
        protected string _textToShow;

        #endregion
    }
}
