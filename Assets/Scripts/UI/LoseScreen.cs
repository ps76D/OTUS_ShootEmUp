using System;
using Infrastructure;
using Infrastructure.DI;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public sealed class LoseScreen : UIScreen
    {
        [Inject]
        private GameBootstrapper _gameBootstrapper;
        
        [SerializeField] private Button _reviveButton;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _exitButton;
        
        private GameStateMachine _gameStateMachine;
        
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

            _uiManager.CloseScreen(this);
        }
        
        private void RestartGame()
        {
            _gameStateMachine.Enter<LoadInGameState>();

            _uiManager.CloseScreen(this);
        }
        
        private void ExitGame()
        {
            _gameStateMachine.Enter<MainMenuState>();

            _uiManager.CloseScreen(this);
        }
    }
}