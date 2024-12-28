using System.Collections.Generic;
using Components;
using Infrastructure.CommonInterfaces;
using Level;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletManager : MonoBehaviour
    {
        [SerializeField] private Bullet _prefab;

        [SerializeField] private Transform _worldTransform;
        
        [SerializeField] private BulletPool _bulletPool;

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
                bullet = Instantiate(_prefab, _worldTransform);
            }

            bullet.UpdateBullet(bulletArgs);

            _bulletPool.CheckIfBulletCollide(bullet);
        }

        public Bullet CreateBullet(Transform container)
        {
            Bullet bullet = Instantiate(_prefab, container);

            return bullet;
        }
    }
}