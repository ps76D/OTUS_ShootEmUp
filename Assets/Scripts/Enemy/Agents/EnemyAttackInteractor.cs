using Components;
using Infrastructure.CommonInterfaces;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackInteractor : MonoBehaviour, IFixedUpdatable
    {
        [SerializeField] private float _attackCooldown;

        private EnemyMoveInteractor _enemyMoveInteractor;
        
        private EnemyWeapon _enemyWeapon;
        
        private HitPointsComponent _attackTarget;

        private float _currentTime;

        private void Awake()
        {
            _enemyMoveInteractor = GetComponent<EnemyMoveInteractor>();
            _enemyWeapon = GetComponent<EnemyWeapon>();
        }

        public void SetTarget(HitPointsComponent target)
        {
            _attackTarget = target;
        }

        public void Reset()
        {
            _currentTime = _attackCooldown;
        }

        public void CustomFixedUpdate()
        {
        }

        public void FixedUpdate()
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
            return _enemyMoveInteractor.IsReached;
        }
        
        private bool CheckPlayerIsAlive()
        {
            return _attackTarget.IsHitPointsExists();
        }
        
        private void EnemyFireWithCooldown()
        {
            _currentTime -= Time.fixedDeltaTime;
			
            if (!(_currentTime <= 0)) return;
            _enemyWeapon.Fire();
			
            _currentTime += _attackCooldown;
        }
    }
}