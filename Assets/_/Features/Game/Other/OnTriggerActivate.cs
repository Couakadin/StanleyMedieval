using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerActivate : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3)
            {
                for (int i = 0; i < _toActivate.Count; i++)
                {
                    print("activating");
                    _toActivate[i].SetActive(true);
                }
                gameObject.SetActive(false);
            }

        }

        [SerializeField] private List<GameObject> _toActivate;
    }
}
