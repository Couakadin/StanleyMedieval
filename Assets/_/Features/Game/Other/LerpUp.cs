using UnityEngine;

namespace Game.Runtime
{
    public class LerpUp : MonoBehaviour
    {
        private void Awake()
        {
            _targetPosition = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
        }
        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition, 6f * Time.deltaTime);
        }

        private Vector3 _targetPosition;
    }
}
