using System;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        public event Action<Bullet, Collision2D> OnCollisionEntered;

        [NonSerialized] public int Damage;

        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void UpdateBullet(BulletArguments bulletArguments)
        {
            this.SetPosition(bulletArguments.Position);
            this.SetColor(bulletArguments.Color);
            this.SetPhysicsLayer(bulletArguments.PhysicsLayer);
            this.Damage = bulletArguments.Damage;
            this.SetVelocity(bulletArguments.Velocity);
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            OnCollisionEntered?.Invoke(this, collision);
        }

        private void SetVelocity(Vector2 velocity)
        {
            this._rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
            this.gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            this.transform.position = position;
        }

        private void SetColor(Color color)
        {
            this._spriteRenderer.color = color;
        }
    }
}