using System.Collections.Generic;
using Components;
using Infrastructure;
using Infrastructure.CommonInterfaces;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletManager
    {
        private readonly Bullet _bulletPrefab;

        private readonly Transform _worldTransform;
        
        private readonly BulletPool _bulletPool;
        
        private readonly UpdateController _updateController;

        public BulletManager(Bullet bullet, BulletPool bulletPool, Transform worldTransform, UpdateController updateController)
        {
            _bulletPrefab = bullet;
            _bulletPool = bulletPool;
            _worldTransform = worldTransform;
            _updateController = updateController;
        }
        
        public void OnFlyBullet(Weapon weapon)
        {
            BulletConfig config = weapon.GetBulletConfig();
            
            BulletFactory(new BulletArguments
            {
                PhysicsLayer = (int) config._physicsLayer,
                Color = config._color,
                Damage = config._damage,
                Position = weapon.GetPosition(),
                Velocity = weapon.CalcBulletVelocity()
            });
        }
        
        private void BulletFactory(BulletArguments bulletArgs)
        {
            if (_bulletPool.BulletPoolLocal.TryDequeue(out var bullet))
            {
                bullet.transform.SetParent(_worldTransform);
            }
            else
            {
                bullet = CreateInstance(_bulletPrefab.gameObject, _worldTransform).GetComponent<Bullet>();
            }

            bullet.UpdateBullet(bulletArgs);

            _bulletPool.CheckIfBulletCollide(bullet);
        }

        public Bullet CreateBullet(Transform container)
        {
            Bullet bullet = CreateInstance(_bulletPrefab.gameObject, container).GetComponent<Bullet>();
            
            _updateController.PoolFixedUpdatable.Add(bullet.GetComponent<Bullet>());

            return bullet;
        }

        private GameObject CreateInstance(GameObject prefab, Transform container)
        {
            return Object.Instantiate(prefab, container);
        }
    }
}