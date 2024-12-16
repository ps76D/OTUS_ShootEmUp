using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPositionsProvider : MonoBehaviour
    {
        [SerializeField] private Transform[] _spawnPositions;
        
        [SerializeField] private Transform[] _attackPositions;

        public Transform RandomSpawnPosition()
        {
            return this.RandomTransform(this._spawnPositions);
        }

        public Transform RandomAttackPosition()
        {
            return this.RandomTransform(this._attackPositions);
        }

        private Transform RandomTransform(Transform[] transforms)
        {
            int index = Random.Range(0, transforms.Length);
            return transforms[index];
        }
    }
}