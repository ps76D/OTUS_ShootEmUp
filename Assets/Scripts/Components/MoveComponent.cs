using Input;
using UnityEngine;

namespace Components
{
    public sealed class MoveComponent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _speed = 5.0f;

        [SerializeField] private bool _isPlayer;
        
        private Vector2 _moveXVector;


        private void OnEnable()
        {
            if (_isPlayer) 
            {
                InputManager.OnPlayerMove += Move;
            }
        }
        
        private void OnDisable()
        {
            if (_isPlayer) 
            {
                InputManager.OnPlayerMove -= Move;
            }
        }

        private void Move(float value)
        {
            CalcMoveXVector(value);
            
            MoveByRigidbodyVelocity(_moveXVector);
        }

        private void CalcMoveXVector(float value)
        {
            _moveXVector = new Vector2(value, 0) * Time.fixedDeltaTime;
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            Vector2 nextPosition = _rigidbody2D.position + vector * _speed;
            _rigidbody2D.MovePosition(nextPosition);
        }
    }
}