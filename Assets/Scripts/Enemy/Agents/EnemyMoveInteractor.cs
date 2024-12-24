using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyMoveInteractor : MonoBehaviour
    {
        public bool IsReached 
        {
            get;
            private set;
        }

        private MoveComponent _moveComponent;

        private Vector2 _destination;
        
        private void Awake()
        {
            _moveComponent =  GetComponent<MoveComponent>();
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }

        private void FixedUpdate()
        {
            MoveEnemyOnPosition();
        }

        private void MoveEnemyOnPosition()
        {
            if (IsReached)
            {
                return;
            }
            
            Vector2 vector = _destination - (Vector2) transform.position;
            if (vector.magnitude <= 0.25f)
            {
                IsReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}