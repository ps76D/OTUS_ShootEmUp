using Infrastructure.DI;
using Input;
using UnityEngine;

namespace Components
{
    public sealed class WeaponComponent : Weapon
    {
        [InjectCustomLocal]
        private InputManager _inputManager;
        
        override protected private void Start()
        {
            base.Start();
            
            _inputManager.OnPlayerFire += Fire;
        }

        override protected private void OnDisable()
        { 
            base.OnDisable();
            
            _inputManager.OnPlayerFire -= Fire;
        }

        public override Vector2 CalcBulletVelocity()
        {
            Vector2 velocity = GetRotation() * Vector3.up * GetBulletConfig()._speed;
            return velocity;
        }
    }
}