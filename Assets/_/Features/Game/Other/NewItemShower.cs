using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class NewItemShower : MonoBehaviour
    {
        private void Awake()
        {
            _image = GetComponent<Image>();
        }

        private void Update()
        {
            if (_fadingIn && _opacityValue < 1)
            {
                _opacityValue += Time.deltaTime;
                _image.color = new Color(1, 1, 1, _opacityValue);
            }
            else if (_onScreen && _timer > 0)
            {
                _fadingIn = false;
                _timer -= Time.deltaTime;
            }
            else if (_onScreen && _opacityValue > 0)
            {
                _opacityValue -= Time.deltaTime;
                _image.color = new Color(1, 1, 1, _opacityValue);
            }
            else
            {
                _onScreen = false;
            }
        }

        public void FadeIn(Sprite image)
        {
            _fadingIn = true;
            _onScreen = true;
            _timer = _timeBeforeFadeOut;
            _image.sprite = image;
        }

        #region Private

        [SerializeField] private float _opacityValue = 1f;
        private Image _image;
        private float _timeBeforeFadeOut = 2f;
        private float _timer;
        private bool _fadingIn;
        private bool _onScreen;

        #endregion
    }
}
