using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class ChangeAnimationMinecart : MonoBehaviour
    {
        public void ChangeAnimation()
        {
            GetComponent<Animator>().SetBool("BridgeDown", true);
        }
    }
}
