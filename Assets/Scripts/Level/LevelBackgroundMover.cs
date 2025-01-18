using System;
using UnityEngine;
using Infrastructure.CommonInterfaces;

namespace Level
{
    public sealed class LevelBackgroundMover : MonoBehaviour, IFixedUpdatable
    {
        [SerializeField] private BackgroundMovementConfig _config;

        private float _startPositionY;

        private float _endPositionY;

        private float _movingSpeedY;

        private Transform _backTransform;

        private Vector3 _startPositionVector;

        private Vector3 _positionDeltaVector;
        
        private void Awake()
        {
            InitializeBackground();
        }
        
        public void CustomFixedUpdate()
        {
            MoveBackground();
        }

        private void InitializeBackground()
        {
            _startPositionY = _config._startPositionY;
            _endPositionY = _config._endPositionY;
            _movingSpeedY = _config._movingSpeedY;
            
            _backTransform = transform;
            Vector3 position = _backTransform.position;
            
            _startPositionVector = position;
            _startPositionVector.y = _startPositionY;
            
            _positionDeltaVector = new Vector3();
        }

        private void MoveBackground()
        {
            if (_backTransform.position.y <= _endPositionY)
            {
                _backTransform.position = _startPositionVector;
            }

            CalculateYPositionDelta();
            
            _backTransform.position -= _positionDeltaVector;
        }

        private void CalculateYPositionDelta()
        {
            float positionYDelta = _movingSpeedY * Time.fixedDeltaTime;
            
            _positionDeltaVector.y = positionYDelta;
        }


        [Serializable]
        public sealed class BackgroundMovementConfig
        {
            [SerializeField]
            public float _startPositionY;

            [SerializeField]
            public float _endPositionY;

            [SerializeField]
            public float _movingSpeedY;
        }
    }
}