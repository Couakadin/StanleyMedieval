using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class ChangeAnimationMinecart : MonoBehaviour
    {
        public void ChangeAnimation()
        {
            m_animator.defaultBool = true;
        }

        public AnimatorControllerParameter m_animator;
    }
}
