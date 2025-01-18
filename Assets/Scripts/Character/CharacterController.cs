using System;
using UnityEngine;
using Components;
using Level;

namespace Character
{
    public sealed class CharacterController : IDisposable
    {
        public HitPointsComponent Character {
            get;
        }

        public CharacterController(HitPointsComponent hitPointsComponent)
        {
            Character = hitPointsComponent;
            
            Character.OnHitPointsEmpty += CharacterDeath;
        }

        public event Action OnCharacterDeath;
        
        private void CharacterDeath(HitPointsComponent _)
        {
            OnCharacterDeath?.Invoke();
        }

        public void Dispose()
        {
            Character.OnHitPointsEmpty -= CharacterDeath;
        }
    }
}