using System;
using Infrastructure.CommonInterfaces;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour, IFixedUpdatable
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public int Damage;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        private Vector2 _velocity;
        private Vector3 _direction;

        public void UpdateBullet(BulletArguments bulletArguments)
        {
            SetPosition(bulletArguments.Position);
            SetColor(bulletArguments.Color);
            SetPhysicsLayer(bulletArguments.PhysicsLayer);
            Damage = bulletArguments.Damage; 
            _velocity = bulletArguments.Velocity;
            _direction = new Vector3(_velocity.x, _velocity.y, 0);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        private void MoveBullet()
        {
            transform.position += _direction * Time.deltaTime;
        }
        
        public void CustomFixedUpdate()
        {
            MoveBullet();
        }
    }
}