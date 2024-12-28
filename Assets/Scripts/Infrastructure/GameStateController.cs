using System;
using System.Collections.Generic;
using Infrastructure.DI;
using Infrastructure.Listeners;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class GameStateController : MonoBehaviour
    {
        [Inject]
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        
        /*[InjectIEnumerable]
        private IEnumerable<IGameStateListener> _gameStateListeners;*/
        
        [InjectIEnumerableLocal]
        private IEnumerable<IGameStateListener> _gameStateListenersLocal;

        private GameStateMachine _gameStateMachine;

        private void Awake()
        {
            DontDestroyOnLoad(this);
                
            /*_gameBootstrapper = FindObjectOfType<GameBootstrapper>();*/
        }
        
        private void OnEnable()
        {
            _gameBootstrapper.Game.StateMachine.GetState<PauseState>().OnPauseState += PauseGame;
            _gameBootstrapper.Game.StateMachine.GetState<LoseState>().OnLoseState += FinishGame;
            _gameBootstrapper.Game.StateMachine.GetState<GameLoopState>().OnGameLoopState += InGame;
            _gameBootstrapper.Game.StateMachine.GetState<MainMenuState>().OnMainMenuState += StartGame;
        }
        
        private void OnDisable()
        {
            _gameBootstrapper.Game.StateMachine.GetState<PauseState>().OnPauseState -= PauseGame;
            _gameBootstrapper.Game.StateMachine.GetState<LoseState>().OnLoseState -= FinishGame;
            _gameBootstrapper.Game.StateMachine.GetState<GameLoopState>().OnGameLoopState -= InGame;
            _gameBootstrapper.Game.StateMachine.GetState<MainMenuState>().OnMainMenuState -= StartGame;
        }

        private void StartGame()
        {
            /*foreach (var listener in _gameStateListenersLocal)
            {
                if (listener is IStartGameListener currentListener)
                {
                    currentListener.StartGame();
                }
            }*/
            
            /*foreach (var listener in _gameStateListeners)
            {
                if (listener is IStartGameListener currentListener)
                {
                    currentListener.StartGame();
                }
            }*/
        }
        
        private void InGame()
        {
            /*foreach (var listener in _gameStateListeners)
            {
                if (listener is IInGameListener currentListener)
                {
                    currentListener.InGame();
                }
            }*/
            
            foreach (var listener in _gameStateListenersLocal)
            {
                if (listener is IInGameListener currentListener)
                {
                    currentListener.InGame();
                }
            }
        }

        private void PauseGame()
        {
            foreach (var listener in _gameStateListenersLocal)
            {
                if (listener is IPauseGameListener currentListener)
                {
                    currentListener.PauseGame();
                }
            }
            
            /*foreach (var listener in _gameStateListeners)
            {
                if (listener is IPauseGameListener currentListener)
                {
                    currentListener.PauseGame();
                }
            }*/
        }

        private void FinishGame()
        {
            foreach (var listener in _gameStateListenersLocal)
            {
                if (listener is IFinishGameListener currentListener)
                {
                    currentListener.FinishGame();
                }
            }
            
            /*foreach (var listener in _gameStateListeners)
            {
                if (listener is IFinishGameListener currentListener)
                {
                    currentListener.FinishGame();
                }
            }*/
        }
    }
}