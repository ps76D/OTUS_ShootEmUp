using System;
using UnityEngine;

namespace Level
{
    public sealed class LevelBackgroundMover : MonoBehaviour
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
            this.InitializeBackground();
        }

        private void FixedUpdate()
        {
            this.MoveBackground();
        }

        private void InitializeBackground()
        {
            this._startPositionY = this._config._startPositionY;
            this._endPositionY = this._config._endPositionY;
            this._movingSpeedY = this._config._movingSpeedY;
            
            this._backTransform = this.transform;
            Vector3 position = this._backTransform.position;
            
            this._startPositionVector = position;
            this._startPositionVector.y = this._startPositionY;
            
            this._positionDeltaVector = new Vector3();
        }

        private void MoveBackground()
        {
            if (this._backTransform.position.y <= this._endPositionY)
            {
                this._backTransform.position = this._startPositionVector;
            }

            this.CalculateYPositionDelta();
            
            this._backTransform.position -= this._positionDeltaVector;
        }
        
        private void CalculateYPositionDelta()
        {
            float positionYDelta = this._movingSpeedY * Time.fixedDeltaTime;
            
            this._positionDeltaVector.y = positionYDelta;
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