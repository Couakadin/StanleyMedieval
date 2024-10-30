using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PushForward : MonoBehaviour
    {
        #region Unity API

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }
        private void OnEnable()
        {
            _rb.AddForce(transform.forward * _pushStrength);
        }

        #endregion

        #region Privates

        [SerializeField] private float _pushStrength = 5f;
        private Rigidbody _rb;

        #endregion
    }
}
