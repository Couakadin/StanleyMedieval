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
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        OnSuccess();
                    }
                }
                else if (_itemBlackboard.ContainsKey(_itemRequired.m_name))
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        OnSuccess();
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 16)
            {
                OnSuccess();
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
            else
                _audioReader.AudioSet(_clips);

            if (_itemGained != null)
                _playerBlackboard.SetValue(_itemGained.m_name, _itemGained);
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

        private int _clipIndex;

        #endregion
    }
}
