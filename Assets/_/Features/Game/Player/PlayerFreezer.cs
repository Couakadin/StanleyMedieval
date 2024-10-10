using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerFreezer : MonoBehaviour
    {
        private void Update()
        {
            if (_wasFrozen && _frozenDuration >-1)
            {
                _frozenDuration -= Time.deltaTime;

                if (_frozenDuration < 0)
                    _controller.UnfreezePlayer();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_wasFrozen)
            {
                FreezePlayerMovement();
                _wasFrozen = true;
            }
        }

        public void FreezePlayerMovement()
        {
            _controller.FreezePlayer();
        }

        [SerializeField] private PlayerController _controller;
        [SerializeField] private float _frozenDuration;

        private bool _wasFrozen;
    }
}
