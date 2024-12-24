using System;
using Infrastructure;
using Infrastructure.DI;
using UI.Infrastructure;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public sealed class MainMenuScreen : UIScreen
    {
        [Inject]
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
            _gameStateMachine.Enter<GameLoopState>();
            
            _uiManager.CloseScreen(this);
        }
    }
}