using System;
using Infrastructure;
using Infrastructure.DI;
using UI.Infrastructure;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class MainMenuScreen : UIScreen, IStartGameListener
    {
        [Inject]
        private GameBootstrapper _gameBootstrapper;

        [SerializeField] private Button _startButton;

        private GameStateMachine _gameStateMachine;

        public void Start()
        {
            this._gameStateMachine = this._gameBootstrapper.Game.StateMachine;
            
            this._startButton.onClick.AddListener(this.StartGame);
        }

        private void StartGame()
        {
            this._gameStateMachine.Enter<GameLoopState>();
            
            UIManager.CloseScreen(this);
        }
    }
}