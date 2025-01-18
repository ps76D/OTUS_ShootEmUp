using System;
using Enemy;
using UnityEngine;
using Zenject;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [Inject]
        [SerializeField] private EnemyManager _enemyManager;
            
        [SerializeField] private int _startHitPoints;
        [SerializeField] private Collider2D _collider;

        private int _hitPoints;
        
        public event Action<HitPointsComponent> OnHitPointsEmpty;
        public event Action<HitPointsComponent> OnHitPointsChanged;

        private void Start()
        {
            OnHitPointsEmpty += _enemyManager.OnDestroyed;
            
            ResetHitPoints();
        }

        private void OnDestroy()
        {
            OnHitPointsEmpty -= _enemyManager.OnDestroyed;
        }

        public bool IsHitPointsExists() {
            return _hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            _hitPoints -= damage;
            OnHitPointsChanged?.Invoke(this);
            
            if (_hitPoints <= 0)
            {
                OnHitPointsEmpty?.Invoke(this);
            }
        }

        public void ResetHitPoints()
        {
            _hitPoints = _startHitPoints;
            OnHitPointsChanged?.Invoke(this);
        }
        
        public void Revive()
        {
            ResetHitPoints();
            OnHitPointsChanged?.Invoke(this);
        }

        public int GetCurrentHitPointsValue()
        {
            return _hitPoints;
        }

        public void TurnOnOffCollider(bool value)
        {
            _collider.enabled = value;
        }
    }
}