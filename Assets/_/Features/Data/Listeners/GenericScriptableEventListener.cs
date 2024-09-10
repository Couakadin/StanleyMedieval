using UnityEngine;
using UnityEngine.Events;

namespace Data.Runtime
{
    public class GenericScriptableEventListener<T> : MonoBehaviour
    {
        public UnityEvent<T> m_event;
        public GenericScriptableEvent<T> m_listener;

        private void OnEnable() => m_listener.Subscribe(this);

        private void OnDisable() => m_listener.Unsubscribe(this);

        internal void OnRaiseEvent(T value) => m_event.Invoke(value);
    }
}