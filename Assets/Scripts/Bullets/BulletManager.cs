using System.Collections.Generic;
using Components;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField] private int _initialCount = 50;

        [SerializeField] private Transform _container;

        [SerializeField] private Bullet _prefab;

        [SerializeField] private Transform _worldTransform;

        [SerializeField] private LevelBounds _levelBounds;

        private readonly Queue<Bullet> _bulletPool = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();
        
        private void Awake()
        {
            for (int i = 0; i < _initialCount; i++)
            {
                Bullet bullet = Instantiate(_prefab, _container);
                _bulletPool.Enqueue(bullet);
            }
        }

        private void FixedUpdate()
        {
            _cache.Clear();
            _cache.AddRange(_activeBullets);

            ClearBulletsOutOfBounds();
        }

        private void ClearBulletsOutOfBounds()
        {
            for (int i = 0, count = _cache.Count; i < count; i++)
            {
                Bullet bullet = _cache[i];
                if (!_levelBounds.CheckIsInBounds(bullet.transform.position))
                {
                    RemoveBullet(bullet);
                }
            }
        }
        
        public void OnFlyBullet(Weapon weapon)
        {
            BulletConfig config = weapon.GetBulletConfig();
            
            FlyBulletByArgs(new BulletArguments
            {
                PhysicsLayer = (int) config._physicsLayer,
                Color = config._color,
                Damage = config._damage,
                Position = weapon.GetPosition(),
                Velocity = weapon.CalcBulletVelocity()
            });
        }
        
        private void FlyBulletByArgs(BulletArguments bulletArgs)
        {
            if (_bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = Instantiate(_prefab, _worldTransform);
            }

            bullet.UpdateBullet(bulletArgs);

            CheckIfBulletCollide(bullet);
        }
        
        private void CheckIfBulletCollide(Bullet bullet)
        {
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletDamageInteractor.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (!_activeBullets.Remove(bullet)) return;
            
            bullet.OnCollisionEntered -= OnBulletCollision;
            bullet.transform.SetParent(_container);
            _bulletPool.Enqueue(bullet);
        }
    }
}