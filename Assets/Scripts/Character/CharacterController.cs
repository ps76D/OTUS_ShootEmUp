using System;
using UnityEngine;
using Components;
using Level;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _character;

        public HitPointsComponent Character => _character;

        public event Action OnCharacterDeath;

        private void OnEnable()
        {
            _character.OnHitPointsEmpty += CharacterDeath;
        }

        private void OnDisable()
        {
            _character.OnHitPointsEmpty -= CharacterDeath;
        }

        private void CharacterDeath(HitPointsComponent _)
        {
            OnCharacterDeath?.Invoke();
        }
    }
}