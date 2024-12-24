using System;
using Infrastructure;
using Infrastructure.DI;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class MainMenuScreen : UIScreen
    {
        [InjectCustom]
        private GameBootstrapper _gameBootstrapper;

        [SerializeField] private Button _startButton;

        private GameStateMachine _gameStateMachine;

        public void Start()
        {
            _gameStateMachine = _gameBootstrapper.Game.StateMachine;
            
            _startButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            _gameStateMachine.Enter<LoadInGameState>();
            
            _uiManager.CloseScreen(this);
        }
    }
}