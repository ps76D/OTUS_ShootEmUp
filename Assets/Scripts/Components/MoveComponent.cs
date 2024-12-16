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
            if (this._isPlayer) 
            {
                InputManager.OnPlayerMove += this.Move;
            }
        }
        
        private void OnDisable()
        {
            if (this._isPlayer) 
            {
                InputManager.OnPlayerMove -= this.Move;
            }
        }

        private void Move(float value)
        {
            this.CalcMoveXVector(value);
            
            this.MoveByRigidbodyVelocity(this._moveXVector);
        }

        private void CalcMoveXVector(float value)
        {
            this._moveXVector = new Vector2(value, 0) * Time.fixedDeltaTime;
        }
        
        public void MoveByRigidbodyVelocity(Vector2 vector)
        {
            Vector2 nextPosition = this._rigidbody2D.position + vector * this._speed;
            this._rigidbody2D.MovePosition(nextPosition);
        }
    }
}