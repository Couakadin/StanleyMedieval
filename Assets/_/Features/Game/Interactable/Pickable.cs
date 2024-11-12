using Data.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class Pickable : MonoBehaviour
    {
        [Header("-- Item Info --")]
        public ItemData m_itemData;

        [Header("-- Audio Parameters --")]
        public List<DialogueScriptableObject> m_pickupDialogue;
        public List<Pickable> m_sharedAudio;
        public int _clipIndex;

        [Header("-- Other --")]
        public List<GameObject> m_toActivate;
        public List<GameObject> m_toDeactivate;
    }
}
