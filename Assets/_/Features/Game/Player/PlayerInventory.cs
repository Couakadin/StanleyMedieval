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
            if (m_items.Count > 1)
            {
                if (Input.GetAxisRaw("Mouse ScrollWheel") < -0.1 && _activeItemId < m_items.Count-1)
                {
                    _activeItemId += 1;
                    ActiveItemChange(_activeItemId);

                }
                else if (Input.GetAxisRaw("Mouse ScrollWheel") > 0.1 && _activeItemId > 0)
                {
                    _activeItemId -= 1;
                    ActiveItemChange(_activeItemId);
                }
            }
            else if (m_items.Count == 1)
            {
                ActiveItemChange(0);
                _selector.SetActive(true);
            }
            else
            {
                m_activeItem = null;
                _selector.SetActive(false);
            }
        }

        #endregion


        #region METHODS

        public void InventoryUpdate()
        {
            print("item changed");
            m_items.Clear();
            m_items = new List<ItemData>();

            foreach (KeyValuePair<string, object> kvp in _itemBlackboard._data)
            {
                if (kvp.Value is ItemData && kvp.Key != "ActiveItem")
                {
                    m_items.Add(kvp.Value as ItemData);
                }
            }

            foreach (Image slot in _inventorySlots)
            {
                slot.gameObject.SetActive(false);
            }

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
        public void ActiveItemChange(int i)
        {
            if (i < m_items.Count)
            {
                m_activeItem = m_items[i];
                _selector.transform.position = _inventorySlots[i].transform.position;
                _itemBlackboard.SetValue("ActiveItem", m_activeItem);
            }
        }

        #endregion

        #region PRIVATE AND PROTECTED

        [SerializeField] private List<Image> _inventorySlots;
        [SerializeField] private GameObject _selector;
        [SerializeField] private VoidScriptableEvent _inventoryUpdateEvent;
        [SerializeField] private VoidScriptableEventistener _inventoryUpdateListener;
        [SerializeField] private Blackboard _itemBlackboard;
        [SerializeField] private int _activeItemId;

        #endregion
    }
}
