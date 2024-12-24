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
            while (_isRunning)
            {
                yield return new WaitForSeconds(1);
            
                GameObject enemy = _enemyPool.SpawnEnemy();
                
                if (enemy == null) continue;
                
                if (!_activeEnemies.Add(enemy)) continue;

                InitializeEnemiesComponents(enemy);
            }
        }
        
        public void StopLoop()
        {
            _isRunning = false;
        }

        private void OnDestroyed(HitPointsComponent enemy)
        {
            if (!_activeEnemies.Remove(enemy.gameObject)) return;
            enemy.GetComponent<HitPointsComponent>().OnHitPointsEmpty -= OnDestroyed;

            _enemyPool.SendEnemyToPool(enemy.gameObject);
        }

        private void InitializeEnemiesComponents(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitPointsEmpty += OnDestroyed;
        }
    }
}