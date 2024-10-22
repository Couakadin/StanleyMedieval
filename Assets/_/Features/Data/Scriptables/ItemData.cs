using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data.Runtime
{
    [CreateAssetMenu(fileName = "Item Data", menuName = "Item Data")]
    public class ItemData : ScriptableObject
    {
        public string m_name;
        public Sprite m_sprite;
        public DialogueScriptableObject m_pickupDialogue;
    }
}
