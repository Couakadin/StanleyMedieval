using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    public class GameManager : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Update()
        {

            if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
            if (Input.GetKeyDown(KeyCode.Backspace))
            {
                Scene scene = SceneManager.GetActiveScene();
                SceneManager.LoadScene(scene.name);
            }
        }

        #endregion

        #region Methods

        #endregion

        #region Utils

        #endregion

        #region Privates

        #endregion
    }
}
