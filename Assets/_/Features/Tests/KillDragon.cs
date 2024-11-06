using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDragon : MonoBehaviour
{
    public void TriggerDragonDeath()
    {
        print("Dragon is dead");
        _dragonAnim.SetBool("Dead", true);
    }

    [SerializeField] private Animator _dragonAnim;
}
