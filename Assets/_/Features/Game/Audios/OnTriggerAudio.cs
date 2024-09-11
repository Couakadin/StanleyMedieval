using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerAudio : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.GetMask("Player") && !_wasPlayed)
            {
                _audio.Play();
                _wasPlayed = true;
            }
        }

        #endregion

        #region Methods

        #endregion

        #region Utils

        #endregion

        #region Privates

        private AudioSource _audio;

        private bool _wasPlayed;

        #endregion
    }
}
