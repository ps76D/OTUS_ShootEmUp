using Infrastructure.DI;
using Input;
using Level;
using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [InjectCustomLocal]
        private InputManager _inputManager;
        
        [InjectCustomLocal]
        [SerializeField] private LevelBounds _levelBounds;
        
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _speed = 5.0f;

        [SerializeField] private bool _isPlayer;
        
        private Vector2 _moveXVector;

        private void Start()
        {
            if (!_isPlayer) return;
                _inputManager.OnPlayerMove += Move;
        }

        private void OnDisable()
        {
            if (!_isPlayer) return;
            _inputManager.OnPlayerMove -= Move;
        }

        private void Move(float value)
        {
            CalcMoveXVector(value);
            
            MoveByRigidbodyVelocity(_moveXVector);
        }

        private void CalcMoveXVector(float value)
        {
            _moveXVector = new Vector3(value, 0) * Time.fixedDeltaTime;
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            Vector3 nextPosition = _rigidbody2D.position + vector * _speed;

            if (_isPlayer)
            {
                if (nextPosition.x <= _levelBounds.GetLeftBorder().position.x 
                    ||  nextPosition.x >= _levelBounds.GetRightBorder().position.x )
                {
                    nextPosition = _rigidbody2D.position;
                }
            }

            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}