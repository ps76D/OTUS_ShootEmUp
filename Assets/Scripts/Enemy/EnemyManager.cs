using System;
using System.Collections;
using System.Collections.Generic;
using Components;
using Enemy.Agents;
using Infrastructure;
using Infrastructure.CommonInterfaces;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public sealed class EnemyManager
    {
        [Inject] private DiContainer _container;

        [Inject]
        private UpdateController _updateController;

        private readonly EnemyPool _enemyPool;

        private readonly GameObject _prefab;

        private readonly Transform _worldTransform;

        private readonly EnemyPositionsProvider _enemyPositionsProvider;

        private readonly Transform _character;

        public EnemyPositionsProvider EnemyPositionsProvider => _enemyPositionsProvider;

        public EnemyManager(EnemyPool enemyPool, GameObject prefab, Transform worldTransform, EnemyPositionsProvider enemyPositionsProvider, Transform target)
        {
            _enemyPool = enemyPool;
            _prefab = prefab;
            _worldTransform = worldTransform;
            _enemyPositionsProvider = enemyPositionsProvider;
            _character = target;
        }
        
        public void OnDestroyed(HitPointsComponent enemy)
        {
            if (!_enemyPool.ActiveEnemies.Remove(enemy.gameObject)) return;
            _enemyPool.SendEnemyToPool(enemy.gameObject);
        }
        
        public GameObject CreateEnemy(Transform container)
        {
            GameObject enemy = _container.InstantiatePrefab(_prefab, container);
            
            _updateController.PoolFixedUpdatable.Add(enemy.GetComponent<EnemyMoveInteractor>());
            _updateController.PoolFixedUpdatable.Add(enemy.GetComponent<EnemyAttackInteractor>());
            
            return enemy;
        }
        
        public GameObject SpawnEnemy()
        {
            if (!_enemyPool.EnemyPoolLocal.TryDequeue(out GameObject enemy))
            {
                return null;
            }

            enemy.transform.SetParent(_worldTransform);

            SpawnPosition spawnPosition = _enemyPositionsProvider.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.transform.position;
            
            AttackPosition attackPosition = _enemyPositionsProvider.RandomAttackPosition();

            EnemyMoveInteractor enemyMoveInteractor = enemy.GetComponent<EnemyMoveInteractor>();
            enemyMoveInteractor.AttackPosition = attackPosition;
            enemyMoveInteractor.AttackPosition._isNotEmpty = true;
            enemyMoveInteractor.SetDestination(attackPosition.transform.position);

            enemy.GetComponent<EnemyAttackInteractor>().SetTarget(_character);
            enemy.GetComponent<EnemyWeapon>().SetTarget(_character);
            
            return enemy;
        }
    }
}