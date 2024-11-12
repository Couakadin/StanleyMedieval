using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDragon : MonoBehaviour
{
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

    [SerializeField] private Animator _dragonAnim;
}
