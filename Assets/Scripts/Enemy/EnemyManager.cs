using System.Collections;
using System.Collections.Generic;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Transform _worldTransform;
        [SerializeField] private EnemyPositionsProvider _enemyPositionsProvider;
        
        [SerializeField] private HitPointsComponent _character;
        
        private void OnDestroyed(HitPointsComponent enemy)
        {
            if (!_enemyPool.ActiveEnemies.Remove(enemy.gameObject)) return;
            enemy.GetComponent<HitPointsComponent>().OnHitPointsEmpty -= OnDestroyed;

            _enemyPool.SendEnemyToPool(enemy.gameObject);
        }

        public void InitializeEnemiesComponents(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHitPointsEmpty += OnDestroyed;
        }
        
        public GameObject CreateEnemy(Transform container)
        {
            GameObject enemy = Instantiate(_prefab, container);

            return enemy;
        }
        
        public GameObject SpawnEnemy()
        {
            if (!_enemyPool.EnemyPoolLocal.TryDequeue(out GameObject enemy))
            {
                return null;
            }

            enemy.transform.SetParent(_worldTransform);

            var spawnPosition = _enemyPositionsProvider.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.transform.position;
            
            var attackPosition = _enemyPositionsProvider.RandomAttackPosition();

            var enemyMoveInteractor = enemy.GetComponent<EnemyMoveInteractor>();
            enemyMoveInteractor.AttackPosition = attackPosition;
            enemyMoveInteractor.AttackPosition._isNotEmpty = true;
            enemyMoveInteractor.SetDestination(attackPosition.transform.position);

            enemy.GetComponent<EnemyAttackInteractor>().SetTarget(_character);
            enemy.GetComponent<EnemyWeapon>().SetTarget(_character);
            
            return enemy;
        }
    }
}