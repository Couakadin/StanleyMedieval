using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    public class MainMenuSceneManager : MonoBehaviour
    {
        public void ChangeToGameScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

        public void LeaveGame()
        {
            Application.Quit();
        }
    }
}
