using Data.Runtime;
using UnityEngine;

namespace Game.Runtime
{
    public class Arrow : MonoBehaviour
    {
        #region Publics

        [HideInInspector]
        public VoidScriptableEvent m_deadEvent;

        #endregion

        #region Unity

        private void Awake()
        {
            _position = transform.position;
        }

        private void OnDisable()
        {
            transform.position = _position;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("WallArrow"))
            {
                gameObject.SetActive(false);
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                m_deadEvent.Raise();
            }
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
