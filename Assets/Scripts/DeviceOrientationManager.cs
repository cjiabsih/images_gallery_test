using System;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class DeviceOrientationManager : MonoBehaviour
    {
        private DeviceOrientation _currentOrientation;
        public DeviceOrientationChangeEvent onOrientationChanged;

        private void Awake()
        {
            _currentOrientation = Input.deviceOrientation;
        }

        private void Update()
        {
            if (Input.deviceOrientation != _currentOrientation)
            {
                _currentOrientation = Input.deviceOrientation;
                onOrientationChanged?.Invoke(_currentOrientation);
            }
        }
    }

    [Serializable]
    public class DeviceOrientationChangeEvent : UnityEvent<DeviceOrientation>
    {
    }
}