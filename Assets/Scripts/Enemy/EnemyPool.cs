using System.Collections;
using System.Collections.Generic;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private EnemyManager _enemyManager;
        
        [Header("Pool")]
        [SerializeField] private int _poolSize = 7;
        [SerializeField] private Transform _container;

        private readonly Queue<GameObject> _enemyPoolLocal = new ();
        private readonly HashSet<GameObject> _activeEnemies = new ();
        
        private bool _isRunning = true;

        public  Queue<GameObject> EnemyPoolLocal => _enemyPoolLocal;
        public HashSet<GameObject> ActiveEnemies => _activeEnemies;
        
        private void Awake()
        {
            InitPool();
        }
        
        private IEnumerator Start()
        {
            while (_isRunning)
            {
                yield return new WaitForSeconds(1);
            
                GameObject enemy = _enemyManager.SpawnEnemy();
                
                if (enemy == null) continue;
                
                if (!_activeEnemies.Add(enemy)) continue;

                _enemyManager.InitializeEnemiesComponents(enemy);
            }
        }
        
        public void StopLoop()
        {
            _isRunning = false;
        }

        private void InitPool()
        {
            for (int i = 0; i < _poolSize; i++)
            {
                GameObject enemy = _enemyManager.CreateEnemy(_container);
                _enemyPoolLocal.Enqueue(enemy);
            }
        }

        public void SendEnemyToPool(GameObject enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyPoolLocal.Enqueue(enemy);
            
            EnemyMoveInteractor enemyMoveInteractor = enemy.GetComponent<EnemyMoveInteractor>();
            
            enemyMoveInteractor.AttackPosition._isNotEmpty = false;


            AttackPosition attackPosition = _enemyManager.EnemyPositionsProvider.RandomAttackPosition();
            enemyMoveInteractor.AttackPosition = attackPosition;


            enemyMoveInteractor.IsReached = false;


            enemy.GetComponent<HitPointsComponent>().ResetHitPoints();
        }
    }
}