using UnityEngine;

namespace Game.Runtime
{
    public class Interactable : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _position = transform.position;
        }

        private void Update()
        {
            Vector3 direction = _position - transform.position;
            float distance = direction.sqrMagnitude;

            if (distance > 20f * 20f) transform.position = _position;
        }

        #endregion

        #region Methods

        #endregion

        #region Utils

        #endregion

        #region Privates

        private Vector3 _position;

        #endregion
    }
}
