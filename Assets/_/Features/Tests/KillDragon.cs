using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillDragon : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            dragonAnim.SetBool("Dead", true);
        }
    }

    public Animator dragonAnim;
}
