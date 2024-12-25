using Components;
using Infrastructure.CommonInterfaces;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyMoveInteractor : MonoBehaviour, IFixedUpdatable
    {
        [SerializeField] private AttackPosition _attackPosition;

        public AttackPosition AttackPosition {
            get => _attackPosition;
            set => _attackPosition = value;
        }

        public bool IsReached 
        {
            get;
            private set;
        }

        private MoveComponent _moveComponent;

        private Vector2 _destination;
        
        private void Awake()
        {
            _moveComponent = GetComponent<MoveComponent>();
        }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }

        public void CustomFixedUpdate()
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