using Data.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class WallBash : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == 16)
            {

                _audioReader.AudioSet(_audioClips);

                if (_toActivate.Count > 0)
                {
                    for (int i = 0; i < _toActivate.Count; i++)
                    {
                        _toActivate[i].SetActive(true);
                    }
                }
                if (_toDeactivate.Count > 0)
                {
                    for (int i = 0; i < _toDeactivate.Count; i++)
                    {
                        _toDeactivate[i].SetActive(false);
                    }
                }
                if (_cameraShakeEvent != null)
                {
                    _cameraShakeEvent.Raise();
                }
            }
        }

        [Header("-- Audio --")]
        [SerializeField] private AudioReader _audioReader;
        [SerializeField] private List<DialogueScriptableObject> _audioClips;
        [Header("-- Stuff to Activate --")]
        [SerializeField] private List<GameObject> _toActivate;
        [SerializeField] private List<GameObject> _toDeactivate;
        [Header("-- Camera --")]
        [SerializeField] private VoidScriptableEvent _cameraShakeEvent;
    }
}
