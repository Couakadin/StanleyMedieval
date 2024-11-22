using Game.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KillDragon : MonoBehaviour
{

    private void Update()
    {
        if (_isEnding)
        {
            _endDuration -= Time.deltaTime;
            if (_endDuration < 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
    public void TriggerDragonAwake()
    {
        _dragonAnim.SetBool("Awake", true);
    }
    public void TriggerDragonSteak()
    {
        _dragonAnim.SetBool("Steak", true);
    }

    public void TriggerDragonRomance()
    {
        _dragonAnim.SetBool("Romance", true);
    }

    public void TriggerDragonUnplug()
    {
        _dragonAnim.SetBool("Unplug", true);
    }

    public void EndGame(float duration)
    {
        _endDuration = duration;
        _isEnding = true;
        _interaction.enabled = false;
    }

    [SerializeField] private Animator _dragonAnim;
    [SerializeField] private InteractDecor _interaction;
    private float _endDuration;
    private bool _isEnding;
}
