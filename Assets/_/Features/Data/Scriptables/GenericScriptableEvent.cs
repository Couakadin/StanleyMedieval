using System.Collections.Generic;
using UnityEngine;

namespace Data.Runtime
{
    [CreateAssetMenu(fileName = "GenericEvent", menuName = "Event/Generic/Generic")]
    public class GenericScriptableEvent<T> : ScriptableObject
    {
        private List<GenericScriptableEventListener<T>> _list = new();
        public void Subscribe(GenericScriptableEventListener<T> _event) =>
            _list.Add(_event);

        public void Unsubscribe(GenericScriptableEventListener<T> _event) =>
            _list.Remove(_event);

        public void Raise(T _value)
        {
            for (var i = _list.Count - 1; i >= 0; i--)
                _list[i].OnRaiseEvent(_value);
        }
    }

    [CreateAssetMenu(fileName = "Vector3Event", menuName = "Event/Generic/Vector3")]

    public class Vector3ScriptableEvent : GenericScriptableEvent<Vector3>
    {

    }

    [CreateAssetMenu(fileName = "FloatEvent", menuName = "Event/Generic/Float")]

    public class FloatScriptableEvent : GenericScriptableEvent<float>
    {

    }

    [CreateAssetMenu(fileName = "IntEvent", menuName = "Event/Generic/Int")]
    public class IntScriptableEvent : GenericScriptableEvent<int>
    {

    }

    [CreateAssetMenu(fileName = "BoolEvent", menuName = "Event/Generic/bool")]
    public class BoolScriptableEvent : GenericScriptableEvent<bool>
    {

    }
}