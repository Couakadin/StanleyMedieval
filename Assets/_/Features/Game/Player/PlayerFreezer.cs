using Data.Runtime;
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
                {
                    _controller.UnfreezePlayer();
                    if (_eventAtEnd != null)
                    {
                        _eventAtEnd.Raise();
                    }
                }
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

            if (_eventAtStart != null)
            {
                _eventAtStart.Raise();
            }
        }

        [SerializeField] private PlayerController _controller;
        [SerializeField] private float _frozenDuration;
        [SerializeField] private VoidScriptableEvent _eventAtEnd;
        [SerializeField] private VoidScriptableEvent _eventAtStart;

        private bool _wasFrozen;
    }
}
