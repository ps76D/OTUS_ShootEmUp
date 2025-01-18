using Components;
using Infrastructure.CommonInterfaces;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackInteractor : MonoBehaviour, IFixedUpdatable
    {
        [SerializeField] private float _attackCooldown;

        private EnemyMoveInteractor _enemyMoveInteractor;
        
        [SerializeField] private EnemyWeapon _enemyWeapon;
        
        private Transform _attackTarget;

        private float _currentTime;

        private void Awake()
        {
            _enemyMoveInteractor = GetComponent<EnemyMoveInteractor>();
            _enemyWeapon = GetComponent<EnemyWeapon>();
        }

        public void SetTarget(Transform target)
        {
            _attackTarget = target;
        }

        public void Reset()
        {
            _currentTime = _attackCooldown;
        }

        public void CustomFixedUpdate()
        {
            if (!CheckEnemyIsOnPosition())
            {
                return;
            }

            if (!CheckPlayerIsAlive())
            {
                return;
            }

            EnemyFireWithCooldown();
        }
        
        private bool CheckEnemyIsOnPosition()
        {
            return _enemyMoveInteractor && _enemyMoveInteractor.IsReached;
        }
        
        private bool CheckPlayerIsAlive()
        {
            return _attackTarget && _attackTarget.GetComponent<HitPointsComponent>().IsHitPointsExists();
        }
        
        private void EnemyFireWithCooldown()
        {
            _currentTime -= Time.fixedDeltaTime;
			
            if (!(_currentTime <= 0)) return;

            if (_enemyWeapon)
            {
                _enemyWeapon.Fire();
            }

			
            _currentTime += _attackCooldown;
        }
    }
}