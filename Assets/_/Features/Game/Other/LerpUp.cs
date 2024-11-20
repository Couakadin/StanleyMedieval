using UnityEngine;

namespace Game.Runtime
{
    public class LerpUp : MonoBehaviour
    {
        private void Awake()
        {
            _targetPosition = new Vector3(transform.position.x, transform.position.y + _distance, transform.position.z);
        }
        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _duration * Time.deltaTime);
        }

        private Vector3 _targetPosition;
        [SerializeField] private int _distance = 1;
        [SerializeField] private float _duration = 3f;
    }
}
