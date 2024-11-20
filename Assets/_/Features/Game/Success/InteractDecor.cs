using Data.Runtime;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class InteractDecor : SuccessAbstract
    {

        #region Unity

        protected void Update()
        {
            if (IsPlayerNear())
            {
                if (_itemRequired == null)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        OnSuccess();
                    }
                }
                else if (_itemBlackboard.GetValue<ItemData>("ActiveItem") == _itemRequired)
                {
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        OnSuccess();
                    }
                }
            }
        }

        #endregion


        #region Methods

        protected override void OnSuccess()
        {
            if (_clipIndex < _clips.Count && _multipleTimes)
            {
                _audioReader.AudioPlay(_clips[_clipIndex]);
                _clipIndex++;
            }
            else if (m_sharedClipIndex < _clips.Count && _sharedIndex.Count > 0)
            {
                _audioReader.AudioPlay(_clips[m_sharedClipIndex]);

                foreach (InteractDecor otherAudio in _sharedIndex)
                    otherAudio.m_sharedClipIndex += 1;
            }
            else
                _audioReader.AudioSet(_clips);

            if (_events.Count > 0)
            {
                for (int i = 0; i < _events.Count; i++)
                {
                    _events[i].Raise();
                }
            }

            if (_itemGained != null)
            {
                _itemBlackboard.SetValue(_itemGained.m_name, _itemGained);
                _inventoryUpdateEvent.Raise();
            }
            if (_itemLost != null)
            {
                _itemBlackboard.RemoveKey(_itemLost.m_name);
                _inventoryUpdateEvent.Raise();
            }

            if (_toActivate.Count > 0)
            {
                for (int i = 0; i < _toActivate.Count; i++)
                {
                    _toActivate[i].SetActive(true);
                }
            }
            if (_toDeactivate.Count > 0)
            {
                for (int i = 0; i < _toDeactivate.Count; i++)
                {
                    _toDeactivate[i].SetActive(false);
                }
            }

            if (_events.Count > 0)
            {
                for (int i = 0; i < _toDeactivate.Count; i++)
                {
                    _events[i].Raise();
                }
            }
        }

        #endregion


        #region Privates

        [Header("-- Needs Item --")]
        [SerializeField] private ItemData _itemRequired;

        [Header("-- Text to show --")]
        [SerializeField] private GameObject _text;

        [Header("-- Audio --")]
        [SerializeField] private AudioReader _audioReader;
        [SerializeField] private bool _multipleTimes;
        [SerializeField] private List<DialogueScriptableObject> _clips;

        [Header("-- Items --")]
        [SerializeField] private Blackboard _itemBlackboard;
        [SerializeField] private ItemData _itemGained;
        [SerializeField] private ItemData _itemLost;
        [SerializeField] private VoidScriptableEvent _inventoryUpdateEvent;

        [Header("-- GameObjects to activate / deactivate --")]
        [SerializeField] private List<GameObject> _toActivate;
        [SerializeField] private List<GameObject> _toDeactivate;

        [Header("-- Events to launch --")]
        [SerializeField] private List<VoidScriptableEvent> _events;

        [Header("-- Comunicating with other items --")]
        [SerializeField] private List<InteractDecor> _sharedIndex;
        public int m_sharedClipIndex;

        private int _clipIndex;

        #endregion
    }
}
