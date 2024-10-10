using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    public class SceneChanger : MonoBehaviour
    {
        private void Update()
        {
            if (_loadScene && !_playerFadeUI.m_fadingOut)
            {
                SceneManager.LoadScene(_scene.buildIndex + 1);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            _playerFadeUI.m_fadingOut = true;
            _scene = SceneManager.GetActiveScene();
            LoadNextScene();
        }

        public void LoadNextScene()
        {
            _loadScene = true;
        }

        [SerializeField] private PlayerFadeUI _playerFadeUI;
        private bool _loadScene;
        private UnityEngine.SceneManagement.Scene _scene;
    }
}
