using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class OnTriggerActivate : MonoBehaviour
    {
        #region Unity API
        private void Update()
        {
            
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 3)
            {
                GameObjectActivation();
                GameObjectDeactivation();

                gameObject.SetActive(false);
            }

        }
        #endregion

        #region Methods

        public void GameObjectActivation()
        {
            for (int i = 0; i < _toActivate.Count; i++)
            {
                _toActivate[i].SetActive(true);
            }
        }

        public void GameObjectDeactivation()
        {
            for (int i = 0; i < _toDeactivate.Count; i++)
            {
                _toDeactivate[i].SetActive(false);
            }
        }

        #endregion


        #region Private and Protected

        [SerializeField] private List<GameObject> _toActivate;
        [SerializeField] private List<GameObject> _toDeactivate;

        private bool _activated;

        #endregion
    }
}
