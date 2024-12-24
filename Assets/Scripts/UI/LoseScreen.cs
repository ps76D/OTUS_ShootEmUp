using System;
using Infrastructure;
using Infrastructure.DI;
using UI.Infrastructure;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class LoseScreen : UIScreen
    {
        [InjectCustom]
        private GameBootstrapper _gameBootstrapper;
        
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        
        private GameStateMachine _gameStateMachine;
        
        public static event Action OnReviveButtonClicked;
        public static event Action OnRestartButtonClicked;
        public static event Action OnExitButtonClicked;
        
        public void Start()
        {            
            _gameStateMachine = _gameBootstrapper.Game.StateMachine;
            
            _reviveButton.onClick.AddListener(Revive);
            _restartButton.onClick.AddListener(RestartGame);
            _exitButton.onClick.AddListener(ExitGame);
        }
        
        private void Revive()
        {
            _gameStateMachine.Enter<GameLoopState>();
            
            OnReviveButtonClicked?.Invoke();

            _uiManager.CloseScreen(this);
        }
        
        private void RestartGame()
        {
            _gameStateMachine.Enter<RestartState>();
            
            OnRestartButtonClicked?.Invoke();

            _uiManager.CloseScreen(this);
        }
        
        private void ExitGame()
        {
            _gameStateMachine.Enter<MainMenuState>();
            
            OnExitButtonClicked?.Invoke();

            _uiManager.CloseScreen(this);
        }
    }
}