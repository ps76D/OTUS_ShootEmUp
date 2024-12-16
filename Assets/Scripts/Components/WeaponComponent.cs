using Input;
using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : Weapon
    {
        override protected private void OnEnable()
        {
            base.OnEnable();
            
            InputManager.OnPlayerFire += this.Fire;
        }

        override protected private void OnDisable()
        { 
            base.OnDisable();
            
            InputManager.OnPlayerFire -= this.Fire;
        }

        public override Vector2 CalcBulletVelocity()
        {
            Vector2 velocity = this.GetRotation() * Vector3.up * this.GetBulletConfig()._speed;
            return velocity;
        }
    }
}