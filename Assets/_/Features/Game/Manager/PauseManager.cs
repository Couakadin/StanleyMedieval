using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    public class PauseManager : MonoBehaviour
    {
        #region UNITY API

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && !_gamePaused) PauseGame(); // Open the pause menu when pressing "escape"
            else if (Input.GetKeyDown(KeyCode.Escape) && _gamePaused) ResumeGame(); // Close the pause menu when pressing "escape"
        }

        #endregion


        #region MAIN METHODS

        public void PauseGame() // ------------ Open the pause menu
        {
            _pauseMenu.SetActive(true);

            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;

            _gamePaused = true;

            _standingCameraController.enabled = false;
            _crouchingCameraController.enabled = false;
            _aimCursor.SetActive(false);
        }
        public void ResumeGame()
        {
            _pauseMenu.SetActive(false);

            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;

            _gamePaused = false;

            _standingCameraController.enabled = true;
            _crouchingCameraController.enabled = true;
            _aimCursor.SetActive(true);
        }
        public void RestartLevel()
        {
            Time.timeScale = 1;
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        public void QuitGame()
        {
            Application.Quit();
        }

        #endregion


        #region PRIVATES

        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _aimCursor;
        [SerializeField] private PlayerCamera _standingCameraController;
        [SerializeField] private PlayerCamera _crouchingCameraController;

        private bool _gamePaused;

        #endregion
    }
}
