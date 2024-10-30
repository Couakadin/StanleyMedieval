using Data.Runtime;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Runtime
{
    public class PlayerFadeUI : MonoBehaviour
    {

        public bool m_fadingIn;
        public bool m_fadingOut;
        public bool m_suddenBlack;

        private void Awake()
        {
            _opacityValue = 1;
        }
        void Update()
        {
            if (m_suddenBlack)
            {
                _blackTimer -= Time.deltaTime;
                if (_blackTimer < 0)
                {
                    m_suddenBlack = false;
                    m_fadingIn = true;
                }
            }


            if (m_fadingOut && _image.color.a <= 1)
            {
                _image.gameObject.SetActive(true);
                m_fadingIn = false;
                FadeOut();
            }
            else if ((m_fadingIn && _image.color.a >= 0))
            {
                m_fadingOut = false;
                FadeIn();
            }
            if (_image.color.a <= 0 && !m_fadingOut)
            {
                m_fadingIn = false;
                _image.gameObject.SetActive(false);
            }
            else if (_image.color.a >= 1 && !m_fadingIn)
            {
                m_fadingOut = false;
            }
        }

        public void FadeIn()
        {
            _opacityValue -= Time.deltaTime;
            _image.color = new Color(0, 0, 0, _opacityValue);
        }

        public void FadeOut()
        {
            _opacityValue += Time.deltaTime;
            _image.color = new Color(0, 0, 0, _opacityValue);
        }

        public void SuddenBlack()
        {
            _image.gameObject.SetActive(true);
            _opacityValue = 1;
            _image.color = new Color(0, 0, 0, 1);
            m_suddenBlack = true;
            _blackTimer = 1f;
        }

        public void EnableCinematic()
        {
            _cinematicBars.SetActive(true);
        }

        public void DisableCinematic()
        {
            _cinematicBars.SetActive(false);
        }

        [SerializeField] private float _opacityValue;
        [SerializeField] private RawImage _image;
        [SerializeField] private VoidScriptableEvent _fadeInEvent;
        [SerializeField] private VoidScriptableEventistener _fadeInListener;
        [SerializeField] private GameObject _cinematicBars;
        private float _blackTimer;
    }
}
