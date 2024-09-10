using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Runtime
{
    public class LockManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject != _key.gameObject) return;
            collision.gameObject.SetActive(false);
            _locked = false;
        }

        private void Update()
        {
            if (_locked) _rigidbody.isKinematic = true;
            else _rigidbody.isKinematic = false;
        }

        #endregion

        #region Methods

        #endregion

        #region Utils

        #endregion

        #region Privates

        [Title("Key")]
        [SerializeField]
        private GameObject _key;
        [SerializeField]
        private bool _locked;

        [Title("Private")]
        private Rigidbody _rigidbody;


        #endregion
    }
}
