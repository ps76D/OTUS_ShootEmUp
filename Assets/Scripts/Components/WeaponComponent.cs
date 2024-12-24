using Input;
using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : Weapon
    {
        override protected private void OnEnable()
        {
            base.OnEnable();
            
            InputManager.OnPlayerFire += Fire;
        }

        override protected private void OnDisable()
        { 
            base.OnDisable();
            
            InputManager.OnPlayerFire -= Fire;
        }

        public override Vector2 CalcBulletVelocity()
        {
            Vector2 velocity = GetRotation() * Vector3.up * GetBulletConfig()._speed;
            return velocity;
        }
    }
}