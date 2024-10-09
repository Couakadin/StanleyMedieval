using Data.Runtime;
using UnityEngine;

namespace Game.Runtime
{
    public class LockDoor : MonoBehaviour
    {
        #region UNITY API

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.isKinematic = true;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.layer == 3)
            {
                foreach (ItemData item in collision.gameObject.GetComponent<PlayerInventory>().m_items)
                {
                    if (item == _requiredItem)
                    {
                        _rb.isKinematic = false;
                    }
                }
            }
        }

        #endregion


        #region PIRVATE AND PROTECTED

        [SerializeField] private ItemData _requiredItem;
        //[SerializeField] private bool _loosesItem;
        private Rigidbody _rb;

        #endregion
    }
}
