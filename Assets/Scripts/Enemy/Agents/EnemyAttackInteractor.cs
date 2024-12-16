using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyAttackInteractor : MonoBehaviour
    {
        [SerializeField] private float _attackCooldown;

        private EnemyMoveInteractor _enemyMoveInteractor;
        
        private EnemyWeapon _enemyWeapon;
        
        private HitPointsComponent _attackTarget;

        private float _currentTime;

        private void Awake()
        {
            this._enemyMoveInteractor = this.GetComponent<EnemyMoveInteractor>();
            this._enemyWeapon = this.GetComponent<EnemyWeapon>();
        }

        public void SetTarget(HitPointsComponent target)
        {
            this._attackTarget = target;
        }

        public void Reset()
        {
            this._currentTime = this._attackCooldown;
        }

        private void FixedUpdate()
        {
            if (!this.CheckEnemyIsOnPosition()) 
            {
                return;
            }
            
            if (!this.CheckPlayerIsAlive()) 
            {
                return;  
            }

            this.EnemyFireWithCooldown();
        }

        private bool CheckEnemyIsOnPosition()
        {
            return this._enemyMoveInteractor.IsReached;
        }
        
        private bool CheckPlayerIsAlive()
        {
            return this._attackTarget.IsHitPointsExists();
        }
        
        private void EnemyFireWithCooldown()
        {
            this._currentTime -= Time.fixedDeltaTime;
			
            if (!(this._currentTime <= 0)) return;
            this._enemyWeapon.Fire();
			
            this._currentTime += this._attackCooldown;
        }
    }
}