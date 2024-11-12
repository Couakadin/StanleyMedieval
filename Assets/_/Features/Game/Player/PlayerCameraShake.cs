using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime
{
    public class PlayerCameraShake : MonoBehaviour
    {
        #region UNITY API

        private void Update()
        {
            if (_cameraStanding.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain >= 0 && _isShaking)
                ResetCameraShake();
            else if (_isShaking && _cameraStanding.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain < 0)
                _isShaking = false;
        }

        #endregion


        #region Methods

        public void StrongCameraShake()
        {
            _cameraStanding.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeIntensity;
            _cameraStanding.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _shakeIntensity;
            _cameraCrouch.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeIntensity;
            _cameraCrouch.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _shakeIntensity;

            _isShaking = true;
        }

        public void SmallCameraShake()
        {
            _cameraStanding.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeIntensity/2;
            _cameraStanding.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _shakeIntensity/2;
            _cameraCrouch.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = _shakeIntensity/2;
            _cameraCrouch.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = _shakeIntensity/2;

            _isShaking = true;
        }

        public void ResetCameraShake()
        {
            _cameraStanding.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain -= Time.deltaTime;
            _cameraStanding.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain -= Time.deltaTime;
            _cameraCrouch.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain -= Time.deltaTime;
            _cameraCrouch.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain -= Time.deltaTime;
        }

        #endregion


        #region Private and portected

        [Header("Cameras")]
        [SerializeField] private CinemachineVirtualCamera _cameraStanding;
        [SerializeField] private CinemachineVirtualCamera _cameraCrouch;
        [SerializeField] private float _shakeIntensity;

        private bool _isShaking;

        #endregion
    }
}
