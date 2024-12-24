using System;
using System.Collections.Generic;
using Infrastructure.DI;
using Infrastructure.Listeners;
using UnityEngine;

namespace Infrastructure
{
    public class GameStateController : MonoBehaviour
    {
        /*[Inject]*/
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        
        /*[Inject]
        [SerializeField] private  IEnumerable<GameBootstrapper> _gameBootstrappers;*/
        
        [InjectIEnumerable]
        private IEnumerable<IGameStateListener> _gameStateListeners;

        private GameStateMachine _gameStateMachine;

        private void Awake()
        {

        }

        /*public GameStateController()
        {
            _gameStateMachine = _gameBootstrapper.Game.StateMachine;
        }*/
        
        private void OnEnable()
        {
            /*foreach (var listener in _gameBootstrappers)
            {
                _gameBootstrapper = listener;
            }*/

            /*_gameStateMachine = GetComponent<GameBootstrapper>().Game.StateMachine;*/
            
            /*_gameBootstrapper.Game.StateMachine.GetState<PauseState>().OnPauseState += PauseGame;
            _gameBootstrapper.Game.StateMachine.GetState<LoseState>().OnLoseState += FinishGame;
            _gameBootstrapper.Game.StateMachine.GetState<GameLoopState>().OnGameLoopState += StartGame;
            _gameBootstrapper.Game.StateMachine.GetState<GameLoopState>().OnGameLoopState += ResumeGame;*/
        }
        
        private void OnDisable()
        {
            /*_gameBootstrapper.Game.StateMachine.GetState<PauseState>().OnPauseState -= PauseGame;
            _gameBootstrapper.Game.StateMachine.GetState<LoseState>().OnLoseState -= FinishGame;
            _gameBootstrapper.Game.StateMachine.GetState<GameLoopState>().OnGameLoopState -= StartGame;
            _gameBootstrapper.Game.StateMachine.GetState<GameLoopState>().OnGameLoopState -= ResumeGame;*/
        }

        private void StartGame()
        {
            foreach (var listener in _gameStateListeners)
            {
                if (listener is IStartGameListener currentListener)
                {
                    currentListener.StartGame();
                }
            }
        }

        private void PauseGame()
        {
            foreach (var listener in _gameStateListeners)
            {
                if (listener is IPauseGameListener currentListener)
                {
                    currentListener.PauseGame();
                }
            }
        }

        private void ResumeGame()
        {
            foreach (var listener in _gameStateListeners)
            {
                if (listener is IResumeGameListener currentListener)
                {
                    currentListener.ResumeGame();
                }
            }
        }

        private void FinishGame()
        {
            foreach (var listener in _gameStateListeners)
            {
                if (listener is IFinishGameListener currentListener)
                {
                    currentListener.FinishGame();
                }
            }
        }
    }
}