using Data.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Runtime
{
    public class LockManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject != _key.gameObject) return;
            collision.gameObject.SetActive(false);
            _locked = false;
            Play(_audioManager, _audioBlackboard.GetValue<AudioClip>("DoorUnlock"));
        }

        private void Update()
        {
            if (_locked) _rigidbody.isKinematic = true;
            else _rigidbody.isKinematic = false;
        }

        #endregion

        #region Methods

        public bool IsLocked() { return _locked; }

        #endregion

        #region Utils

        private void Play(AudioSource audioSource, AudioClip clipToPlay)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
        }

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _audioBlackboard;

        [Title("Key")]
        [SerializeField]
        private GameObject _key;
        [SerializeField]
        private bool _locked;

        [Title("Audios")]
        [SerializeField]
        private AudioSource _audioManager;

        [Title("Private")]
        private Rigidbody _rigidbody;


        #endregion
    }
}
