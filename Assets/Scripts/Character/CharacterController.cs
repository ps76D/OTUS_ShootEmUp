using System;
using UnityEngine;
using Components;

namespace Character
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _character;

        public HitPointsComponent Character => this._character;

        public static event Action OnCharacterDeath;

        private void OnEnable()
        {
            this._character.OnHitPointsEmpty += this.CharacterDeath;
        }

        private void OnDisable()
        {
            this._character.OnHitPointsEmpty -= this.CharacterDeath;
        }

        private void CharacterDeath(HitPointsComponent _)
        {
            OnCharacterDeath?.Invoke();
        }

    }
}