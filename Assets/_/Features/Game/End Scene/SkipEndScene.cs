using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Runtime
{
    public class SkipEndScene : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                SceneManager.LoadScene(0);
            }
        }
    }
}
