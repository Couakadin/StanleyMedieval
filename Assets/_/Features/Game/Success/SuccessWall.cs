using UnityEngine;

namespace Game.Runtime
{
    [RequireComponent(typeof(Animator), typeof(BoxCollider))]
    public class SuccessWall : SuccessAbstract
    {
        #region Publics

        #endregion

        #region Unity

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _boxCollider = GetComponent<BoxCollider>();
        }

        protected void Update()
        {
            if (IsPlayerNear())
            {
                _text.SetActive(true);
                if (Input.GetKeyDown(KeyCode.E))
                    ArchetypeCheck();
            }
            else _text.SetActive(false);
        }

        #endregion

        #region Methods

        protected override void OnSuccess()
        {
            _audioSuccess.Play();
            _animator.SetBool("Fall", true);
            _boxCollider.enabled = false;
        }

        protected override void OnFailure()
        {
            _audioFailure.Play();
            _deathEvent.Raise();
        }

        #endregion

        #region Utils

        #endregion

        #region Privates

        private Animator _animator;
        private BoxCollider _boxCollider;

        [SerializeField]
        private GameObject _text;
        [SerializeField]
        private AudioSource _audioSuccess;
        [SerializeField]
        private AudioSource _audioFailure;

        #endregion
    }
}
