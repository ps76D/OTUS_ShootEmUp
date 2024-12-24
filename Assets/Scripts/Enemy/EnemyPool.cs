using System.Collections.Generic;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPool : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private EnemyPositionsProvider _enemyPositionsProvider;
        
        [SerializeField] private HitPointsComponent _character;
        
        [SerializeField] private Transform _worldTransform;
        
        [Header("Pool")]
        [SerializeField] private Transform _container;

        [SerializeField] private GameObject _prefab;

        private readonly Queue<GameObject> _enemyPool = new ();
        
        private void Awake()
        {
            for (int i = 0; i < 7; i++)
            {
                GameObject enemy = Instantiate(_prefab, _container);
                _enemyPool.Enqueue(enemy);
            }
        }

        public GameObject SpawnEnemy()
        {
            if (!_enemyPool.TryDequeue(out GameObject enemy))
            {
                return null;
            }

            enemy.transform.SetParent(_worldTransform);

            Transform spawnPosition = _enemyPositionsProvider.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            
            Transform attackPosition = _enemyPositionsProvider.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveInteractor>().SetDestination(attackPosition.position);

            enemy.GetComponent<EnemyAttackInteractor>().SetTarget(_character);
            enemy.GetComponent<EnemyWeapon>().SetTarget(_character);
            
            return enemy;
        }

        public void SendEnemyToPool(GameObject enemy)
        {
            enemy.transform.SetParent(_container);
            _enemyPool.Enqueue(enemy);
            
            enemy.GetComponent<HitPointsComponent>().ResetHitPoints();
        }
    }
}