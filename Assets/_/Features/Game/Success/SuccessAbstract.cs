using Data.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public abstract class SuccessAbstract : MonoBehaviour
    {

        #region Utils

        protected bool IsPlayerNear()
        {
            Vector3 playerPosition = _playerBlackboard.GetValue<Vector3>("Position");
            Vector3 directionToObject = transform.position - playerPosition;

            float distanceSqr = directionToObject.sqrMagnitude;
            if (distanceSqr > 2f * 2f) return false;

            Vector3 playerForward = _playerBlackboard.GetValue<Vector3>("Forward");

            directionToObject.Normalize();
            float dotProduct = Vector3.Dot(playerForward, directionToObject);

            //if (dotProduct > 0.6f) return true;

            return true;
        }

        protected virtual void OnSuccess() { }

        protected virtual void OnFailure() { }

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        protected Blackboard _playerBlackboard;

        #endregion
    }
}
