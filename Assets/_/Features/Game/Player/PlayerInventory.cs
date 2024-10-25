using Data.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class PlayerInventory : MonoBehaviour
    {
        #region PUBLICS

        public ItemData m_activeItem;
        public List<ItemData> m_items;

        #endregion


        #region UNITY API

        private void Awake()
        {
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                _inventorySlots[i].gameObject.SetActive(false);
            }
        }
        private void Update()
        {
            for (int i = 0; i < _inventorySlots.Count; i++)
            {
                if (m_items.Count != 0 && i < m_items.Count)
                {
                    if (m_items[i] != null)
                    {
                        _inventorySlots[i].gameObject.SetActive(true);
                        _inventorySlots[i].sprite = m_items[i].m_sprite;
                    }
                    else
                    {
                        _inventorySlots[i].gameObject.SetActive(false);
                    }
                }
            }
        }

        #endregion


        #region PRIVATE AND PROTECTED

        [SerializeField] private List<Image> _inventorySlots;

        #endregion
    }
}
