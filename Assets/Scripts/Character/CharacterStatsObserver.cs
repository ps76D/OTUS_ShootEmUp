using System;
using Components;
using GameManager.Listeners;
using Infrastructure.Listeners;
using UnityEngine;

namespace Character
{
    public sealed class CharacterStatsObserver : MonoBehaviour, ICharacterHitPointsListener
    {
        [SerializeField] private HitPointsComponent _character;
        
        public event Action<int> OnCharacterHitPointsStatsChanged;
        
        private void OnEnable()
        {
            _character.OnHitPointsChanged += InGame;
        }
        
        /*private void SendStatsToUI(HitPointsComponent character)
        {
            OnCharacterHitPointsStatsChanged?.Invoke(character.GetCurrentHitPointsValue());
        }*/
        
        public void InGame(HitPointsComponent hitPointsComponent)
        {
            /*SendStatsToUI(hitPointsComponent);*/
        }
        
    }
}