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

        protected virtual void Update()
        {
            if (IsPlayerNear())
            {
                TextToShow();
                if (Input.GetKeyDown(KeyCode.E))
                    ArchetypeCheck();
            }
            else _textCanvas.text = string.Empty;
        }

        #endregion

        #region Methods

        #endregion

        #region Utils

        protected bool IsPlayerNear()
        {
            Vector3 direction = _playerBlackboard.GetValue<Vector3>("Position") - transform.position;
            float directionSqr = direction.sqrMagnitude;

            if (directionSqr < 2f * 2f) return true;
            return false;
        }

        protected void TextToShow() => _textCanvas.text = _textToShow;

        protected void ArchetypeCheck()
        {
            if (_playerBlackboard.GetValue<PlayerArchetype.Archetype>("Archetype") == (PlayerArchetype.Archetype)_archetypeSuccess)
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

        [Title("Values")]
        [SerializeField]
        protected int _archetypeSuccess;
        [SerializeField]
        protected TextMeshProUGUI _textCanvas;
        [SerializeField]
        protected string _textToShow;

        #endregion
    }
}
