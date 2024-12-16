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
            for (int i = 0; i < this._initialCount; i++)
            {
                Bullet bullet = Instantiate(this._prefab, this._container);
                this._bulletPool.Enqueue(bullet);
            }
        }

        private void FixedUpdate()
        {
            this._cache.Clear();
            this._cache.AddRange(this._activeBullets);

            this.ClearBulletsOutOfBounds();
        }

        private void ClearBulletsOutOfBounds()
        {
            for (int i = 0, count = this._cache.Count; i < count; i++)
            {
                Bullet bullet = this._cache[i];
                if (!this._levelBounds.CheckIsInBounds(bullet.transform.position))
                {
                    this.RemoveBullet(bullet);
                }
            }
        }
        
        public void OnFlyBullet(Weapon weapon)
        {
            BulletConfig config = weapon.GetBulletConfig();
            
            this.FlyBulletByArgs(new BulletArguments
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
            if (this._bulletPool.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(this._worldTransform);
            }
            else
            {
                bullet = Instantiate(this._prefab, this._worldTransform);
            }

            bullet.UpdateBullet(bulletArgs);

            this.CheckIfBulletCollide(bullet);
        }
        
        private void CheckIfBulletCollide(Bullet bullet)
        {
            if (this._activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += this.OnBulletCollision;
            }
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletDamageInteractor.DealDamage(bullet, collision.gameObject);
            this.RemoveBullet(bullet);
        }

        private void RemoveBullet(Bullet bullet)
        {
            if (!this._activeBullets.Remove(bullet)) return;
            
            bullet.OnCollisionEntered -= this.OnBulletCollision;
            bullet.transform.SetParent(this._container);
            this._bulletPool.Enqueue(bullet);
        }
    }
}