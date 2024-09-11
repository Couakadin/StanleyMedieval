using Data.Runtime;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class ArrowPool : MonoBehaviour
    {
        #region Publics

        #endregion

        #region Unity

        private void Start()
        {
            _arrow = ArrowInstantiate();
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer > _counterBlackboard.GetValue<int>("ArrowTimer"))
            {
                GameObject arrow = GetArrowAvailable();
                if (arrow != null)
                {
                    arrow.transform.position = transform.position;
                    arrow.SetActive(true);
                    arrow.GetComponent<Rigidbody>().velocity = transform.forward * 10f;
                }
                _timer = 0f;
            }
        }

        #endregion

        #region Methods

        public GameObject GetArrowAvailable()
        {
            for (int i = 0; i < _arrowList.Count; i++)
            {
                if (_arrowList[i].activeSelf) continue;
                return _arrowList[i];
            }
            return ArrowInstantiate();
        }

        #endregion

        #region Utils

        private GameObject ArrowInstantiate()
        {
            GameObject arrow = Instantiate(_arrowPrefab, transform.position, Quaternion.identity, transform);
            arrow.transform.position = transform.position;
            arrow.transform.rotation = transform.rotation;
            arrow.GetComponent<Arrow>().m_deadEvent = _deadEvent;
            arrow.SetActive(false);
            _arrowList.Add(arrow);

            return arrow;
        }

        #endregion

        #region Privates

        [Title("Data")]
        [SerializeField]
        private Blackboard _counterBlackboard;

        [Title("Events")]
        [SerializeField]
        private VoidScriptableEvent _deadEvent;

        [Title("Settings")]
        [SerializeField]
        private GameObject _arrowPrefab;
        [SerializeField]
        private List<GameObject> _arrowList;

        private GameObject _arrow;

        private float _timer;

        #endregion
    }
}
