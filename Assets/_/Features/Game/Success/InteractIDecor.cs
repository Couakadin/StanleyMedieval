using Data.Runtime;
using UnityEngine;

namespace Game.Runtime
{
    public class InteractIDecor : SuccessAbstract
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
            _triggerAudioList.AudioTypeCheck(_playerBlackboard.GetValue<GameObject>("Player"));

            if (_itemGained != null)
                _playerBlackboard.GetValue<GameObject>("Player").GetComponent<PlayerInventory>().m_items.Add(_itemGained);
        }

        #endregion


        #region Privates

        [SerializeField] private GameObject _text;
        [SerializeField] private OnTriggerAudioList _triggerAudioList;
        [SerializeField] private ItemData _itemGained;

        #endregion
    }
}
