using System;
using Infrastructure;
using Infrastructure.DI;
using UI.Infrastructure;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class LoseScreen : UIScreen, IFinishGameListener
    {
        [Inject]
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
            this._gameStateMachine = this._gameBootstrapper.Game.StateMachine;
            
            this._reviveButton.onClick.AddListener(this.Revive);
            this._restartButton.onClick.AddListener(this.RestartGame);
            this._exitButton.onClick.AddListener(this.ExitGame);
        }
        
        private void Revive()
        {
            OnReviveButtonClicked?.Invoke();

            UIManager.CloseScreen(this);
        }
        
        private void RestartGame()
        {
            OnRestartButtonClicked?.Invoke();

            UIManager.CloseScreen(this);
        }
        
        private void ExitGame()
        {
            this._gameStateMachine.Enter<MainMenuState>();
            
            OnExitButtonClicked?.Invoke();

            UIManager.CloseScreen(this);
        }
    }
}