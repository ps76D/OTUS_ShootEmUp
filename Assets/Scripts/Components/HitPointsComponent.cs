using System;
using UnityEngine;

namespace Components
{
    public sealed class HitPointsComponent : MonoBehaviour
    {
        [SerializeField] private int _startHitPoints;

        private int _hitPoints;
        
        public event Action<HitPointsComponent> OnHitPointsEmpty;
        public event Action<HitPointsComponent> OnHitPointsChanged;

        private void Awake()
        {
            this.ResetHitPoints();
        }

        public bool IsHitPointsExists() {
            return this._hitPoints > 0;
        }

        public void TakeDamage(int damage)
        {
            this._hitPoints -= damage;
            OnHitPointsChanged?.Invoke(this);
            
            if (this._hitPoints <= 0)
            {
                OnHitPointsEmpty?.Invoke(this);
            }
        }

        public void ResetHitPoints()
        {
            this._hitPoints = this._startHitPoints;
            OnHitPointsChanged?.Invoke(this);
        }
        
        public void Revive()
        {
            this.ResetHitPoints();
            OnHitPointsChanged?.Invoke(this);
        }

        public int GetCurrentHitPointsValue()
        {
            return this._hitPoints;
        }
    }
}