using TMPro;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerAudio : MonoBehaviour
    {
        #region Unity

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player") && !_wasPlayed)
            {
                _tmp.GetComponent<TextCleaner>().m_resetTimer = _textStayDuration;
                _audio.clip = _clip;
                _audio.Play();
                _tmp.text = _audioText;
                _wasPlayed = true;
            }
        }

        #endregion

        #region Privates

        [Header ("-- Audio --")]
        [SerializeField] private AudioClip _clip;
        [SerializeField] private AudioSource _audio;

        [Header("-- Text --")]
        [SerializeField] private int _textStayDuration;
        [SerializeField] private string _audioText;
        [SerializeField] private TMP_Text _tmp;

        private bool _wasPlayed;

        #endregion
    }
}
