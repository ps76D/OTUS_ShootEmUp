using Character;
using Components;
using Infrastructure;
using Infrastructure.DI;
using Infrastructure.Listeners;
using TMPro;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using CharacterController = Character.CharacterController;

namespace UI
{
    public sealed class HUDScreen : UIScreen
    {
        [Inject]
        private GameBootstrapper _gameBootstrapper;
        
        [InjectOptional]
        [SerializeField] private CharacterController _characterController;
        
        [SerializeField] private TMP_Text _hitPointsCount;
        [SerializeField] private Button _pauseButton;
        
        private GameStateMachine _gameStateMachine;
        
        public void Start()
        {
            _gameStateMachine = _gameBootstrapper.Game.StateMachine;
            
            _pauseButton.onClick.AddListener(ShowPauseScreen);
        }
        
        private void OnEnable()
        {
            UpdateHitPointsCount(_characterController.Character);
            
            _characterController.Character.OnHitPointsChanged += UpdateHitPointsCount;
        }

        private void OnDisable()
        {
            _characterController.Character.OnHitPointsChanged -= UpdateHitPointsCount;
        }

        private void UpdateHitPointsCount(HitPointsComponent hitPointsComponent)
        {
            int currentValue = hitPointsComponent.GetCurrentHitPointsValue();

            currentValue = currentValue <= 0 ? 0 : hitPointsComponent.GetCurrentHitPointsValue();

            _hitPointsCount.text = currentValue.ToString();
        }

        private void ShowPauseScreen()
        {
            _gameStateMachine.Enter<PauseState>();
        }
    }
}