using System;
using Infrastructure;
using Infrastructure.DI;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public sealed class PauseScreen : UIScreen
    {
        [Inject]
        private GameBootstrapper _gameBootstrapper;
        
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        
        private GameStateMachine _gameStateMachine;
        
        public void Start()
        {
            _gameStateMachine = _gameBootstrapper.Game.StateMachine;
            
            _resumeButton.onClick.AddListener(ResumeGame);
            _exitButton.onClick.AddListener(ExitGame);
        }
        
        private void ResumeGame()
        {
            _gameStateMachine.Enter<GameLoopState>();
            _uiManager.CloseScreen(this);
        }
        
        private void ExitGame()
        {
            _gameStateMachine.Enter<MainMenuState>();
            
            _uiManager.CloseScreen(this);
        }
    }
}