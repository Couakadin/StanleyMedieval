using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class TextCleaner : MonoBehaviour
    {
        #region publics

        public float m_resetTimer;

        #endregion

        #region UNITY API
        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }
        private void Update()
        {
            m_resetTimer -= Time.deltaTime;

            if (m_resetTimer < 0) 
            {
                _text.text = string.Empty;
            }
        }

        #endregion

        #region PRIVATE AND PROTECTED

        private TMP_Text _text;

        #endregion
    }
}
