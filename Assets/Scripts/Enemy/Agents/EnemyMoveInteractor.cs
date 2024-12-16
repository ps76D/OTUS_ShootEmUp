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
            this._moveComponent =  this.GetComponent<MoveComponent>();
        }

        public void SetDestination(Vector2 endPoint)
        {
            this._destination = endPoint;
            this.IsReached = false;
        }

        private void FixedUpdate()
        {
            this.MoveEnemyOnPosition();
        }

        private void MoveEnemyOnPosition()
        {
            if (this.IsReached)
            {
                return;
            }
            
            Vector2 vector = this._destination - (Vector2) this.transform.position;
            if (vector.magnitude <= 0.25f)
            {
                this.IsReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            this._moveComponent.MoveByRigidbodyVelocity(direction);
        }
    }
}