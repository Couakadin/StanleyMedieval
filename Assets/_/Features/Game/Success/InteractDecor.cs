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
                _text.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    OnSuccess();
                }
            }
            else _text.SetActive(false);
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
                _playerBlackboard.GetValue<GameObject>("Player").GetComponent<PlayerInventory>().m_items.Add(_itemGained);
        }

        #endregion


        #region Privates

        [SerializeField] private GameObject _text;

        [Header("-- Audio --")]
        [SerializeField] private AudioReader _audioReader;
        [SerializeField] private bool _multipleTimes;
        [SerializeField] private List<DialogueScriptableObject> _clips;

        [SerializeField] private ItemData _itemGained;

        private int _clipIndex;

        #endregion
    }
}
