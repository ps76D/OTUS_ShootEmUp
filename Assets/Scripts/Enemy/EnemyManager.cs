using System.Collections;
using System.Collections.Generic;
using Components;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        
        private readonly HashSet<GameObject> _activeEnemies = new ();
        
        private bool _isRunning = true;
        
        private IEnumerator Start()
        {
            while (this._isRunning)
            {
                yield return new WaitForSeconds(1);
            
                GameObject enemy = this._enemyPool.SpawnEnemy();
                
                if (enemy == null) continue;
                
                if (!this._activeEnemies.Add(enemy)) continue;

                this.InitializeEnemiesComponents(enemy);
            }
        }
        
        public void StopLoop()
        {
            this._isRunning = false;
        }

        private void OnDestroyed(HitPointsComponent enemy)
        {
            if (!this._activeEnemies.Remove(enemy.gameObject)) return;
            enemy.GetComponent<HitPointsComponent>().OnHitPointsEmpty -= this.OnDestroyed;

            this._enemyPool.SendEnemyToPool(enemy.gameObject);
        }

        private void InitializeEnemiesComponents(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitPointsEmpty += this.OnDestroyed;
        }
    }
}