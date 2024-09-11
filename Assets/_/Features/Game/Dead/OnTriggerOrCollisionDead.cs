using Data.Runtime;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerOrCollisionDead : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.GetMask("Player"))
                _deadEvent.Raise();

        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.GetMask("Player"))
                _deadEvent.Raise();
        }

        #endregion

        #region Methods

        #endregion

        #region Utils

        #endregion

        #region Privates

        [SerializeField]
        private VoidScriptableEvent _deadEvent;

        #endregion
    }
}
