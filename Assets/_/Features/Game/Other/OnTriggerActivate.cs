using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerActivate : MonoBehaviour
    {
        #region Unity API
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3)
            {
                for (int i = 0; i < _toActivate.Count; i++)
                {
                    _toActivate[i].SetActive(true);
                }
                gameObject.SetActive(false);
            }

        }
        #endregion


        #region Private and Protected

        [SerializeField] private List<GameObject> _toActivate;

        #endregion
    }
}
