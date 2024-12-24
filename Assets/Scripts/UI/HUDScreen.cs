using Character;
using Infrastructure;
using Infrastructure.DI;
using TMPro;
using UI.Infrastructure;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class HUDScreen : UIScreen, IInGameListener
    {
        [Inject]
        private GameBootstrapper _gameBootstrapper;
        
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
            CharacterStatsObserver.OnCharacterHitPointsStatsChanged += UpdateHitPointsCount;
        }

        private void OnDisable()
        {
            CharacterStatsObserver.OnCharacterHitPointsStatsChanged -= UpdateHitPointsCount;
        }

        private void UpdateHitPointsCount(int value)
        { 
            int currentValue = value <= 0 ? 0 : value;
            
            _hitPointsCount.text = currentValue.ToString();
        }

        private void ShowPauseScreen()
        {
            _gameStateMachine.Enter<PauseState>();
        }
    }
}