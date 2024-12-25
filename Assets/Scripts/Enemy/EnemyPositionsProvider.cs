using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPositionsProvider : MonoBehaviour
    {
        [SerializeField] private List<SpawnPosition> _spawnPositions;
        
        [SerializeField] private List<AttackPosition> _attackPositions;

        public SpawnPosition RandomSpawnPosition()
        {
            SpawnPosition pos = RandomTransform(_spawnPositions);
            return pos;
        }

        public AttackPosition RandomAttackPosition()
        {
            AttackPosition pos = RandomEmptyPosition();
            return pos;
        }

        private T RandomTransform<T>(List<T> transforms) where T : ScenePosition
        {
            int index = Random.Range(0, transforms.Count);
            return transforms[index];
        }

        private AttackPosition RandomEmptyPosition()
        {
            var sortedEmptyPositions = new List<AttackPosition>();

            for (int index = _attackPositions.Count - 1; index >= 0; index--)
            {
                AttackPosition position = _attackPositions[index];
                if (!position._isNotEmpty)
                {
                    sortedEmptyPositions.Add(position);
                }
            }

            return RandomTransform(sortedEmptyPositions);
        }
    }
}