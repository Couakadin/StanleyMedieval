using UnityEngine;
using UnityEngine.Events;

namespace Data.Runtime
{
    public class VoidScriptableEventistener : MonoBehaviour
    {
        public UnityEvent m_event;
        public VoidScriptableEvent m_listener;

        private void OnEnable() => m_listener.Subscribe(this);

        private void OnDisable() => m_listener.UnSubscribe(this);

        internal void OnRaiseEvent() => m_event.Invoke();
    }
}