using System.Collections.Generic;
using UnityEngine;

namespace Data.Runtime
{
    [CreateAssetMenu(fileName = "VoidEvent", menuName = "Event/Void")]
    public class VoidScriptableEvent : ScriptableObject
    {
        private List<VoidScriptableEventistener> _list = new();
        public void Subscribe(VoidScriptableEventistener _event) =>
            _list.Add(_event);

        public void UnSubscribe(VoidScriptableEventistener _event) =>
            _list.Remove(_event);

        public void Raise()
        {
            for (var i = _list.Count - 1; i >= 0; i--)
                _list[i].OnRaiseEvent();
        }
    }
}