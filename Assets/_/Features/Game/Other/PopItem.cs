using UnityEngine;

namespace Game.Runtime
{
    public class PopItem : MonoBehaviour
    {
        #region Unity API

        private void Awake()
        {
            transform.localScale = Vector3.zero;
        }
        private void Update()
        {
            if (transform.localScale.x <= 0.95f && _popping)
                transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, _popSpeed * Time.deltaTime);
        }

        #endregion


        #region Main Methods

        public void PopTheItem(float popSpeed)
        {
            _popping = true;
            _popSpeed = popSpeed;
        }

        #endregion


        #region Private and protected

        private float _popSpeed = 1f;
        private bool _popping;

        #endregion
    }
}
