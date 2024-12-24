using System;
using Components;
using UnityEngine;

namespace Character
{
    public sealed class CharacterStatsObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _character;
        
        public static event Action<int> OnCharacterHitPointsStatsChanged;
        
        private void OnEnable()
        {
            _character.OnHitPointsChanged += SendStatsToUI;
        }

        private void SendStatsToUI(HitPointsComponent character)
        {
            OnCharacterHitPointsStatsChanged?.Invoke(character.GetCurrentHitPointsValue());
        }
        
    }
}