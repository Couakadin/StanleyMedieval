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
                print("Bridge Down");
                //BridgeDown();
                _event.Raise();
            }
        }

        #endregion

        public void BridgeDown()
        {
            _bridpeUpCollider.SetActive(false);
            _bridgeDownCollider.SetActive(true);
            _animator.SetBool("Fall", true);
        }

        #region PRIVATE

        [SerializeField] private GameObject _bridpeUpCollider;
        [SerializeField] private GameObject _bridgeDownCollider;
        [SerializeField] private Animator _animator;
        [SerializeField] private VoidScriptableEvent _event;

        #endregion
    }
}
