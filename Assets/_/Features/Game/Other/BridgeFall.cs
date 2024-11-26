using Data.Runtime;
using UnityEngine;

namespace Game.Runtime
{
    public class BridgeFall : MonoBehaviour
    {
        #region UNITY API

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 17) 
            {
                BridgeDown();
                _event.Raise();
            }
        }

        #endregion

        public void BridgeDown()
        {
            GetComponent<Rigidbody>().isKinematic = false;
            _fxReader.AudioPlay(_audioClip);
        }

        #region PRIVATE

        [SerializeField] private GameObject _objectToActivate;
        [SerializeField] private GameObject _objectToDeactivate;
        [SerializeField] private Animator _animator;
        [SerializeField] private VoidScriptableEvent _event;
        [SerializeField] private FXReader _fxReader;
        [SerializeField] private AudioClip _audioClip;

        #endregion
    }
}
