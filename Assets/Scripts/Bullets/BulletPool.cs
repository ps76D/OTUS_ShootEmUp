using System;
using System.Collections.Generic;
using Infrastructure.CommonInterfaces;
using Level;
using UnityEngine;

namespace Bullets
{
    public class BulletPool : MonoBehaviour, IFixedUpdatable
    {
        [SerializeField] private BulletManager _bulletManager;
        
        [SerializeField] private int _initialCount = 50;
        [SerializeField] private Transform _container;
        
        [SerializeField] private LevelBounds _levelBounds;
        
        private readonly Queue<Bullet> _bulletPoolLocal = new();
        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _cache = new();

        public Queue<Bullet> BulletPoolLocal => _bulletPoolLocal;

        private void Awake()
        {
            InitPool();
        }

        private void InitPool()
        {
            for (int i = 0; i < _initialCount; i++)
            {
                Bullet bullet = _bulletManager.CreateBullet(_container);
                
                _bulletPoolLocal.Enqueue(bullet);
            }
        }

        public void CustomFixedUpdate()
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
        
        private void RemoveBullet(Bullet bullet)
        {
            if (!_activeBullets.Remove(bullet)) return;
            
            bullet.OnCollisionEntered -= OnBulletCollision;
            bullet.transform.SetParent(_container);
            _bulletPoolLocal.Enqueue(bullet);
        }
        
        private void OnBulletCollision(Bullet bullet, Collision2D collision)
        {
            BulletDamageInteractor.DealDamage(bullet, collision.gameObject);
            RemoveBullet(bullet);
        }
        
        public void CheckIfBulletCollide(Bullet bullet)
        {
            if (_activeBullets.Add(bullet))
            {
                bullet.OnCollisionEntered += OnBulletCollision;
            }
        }
    }
}