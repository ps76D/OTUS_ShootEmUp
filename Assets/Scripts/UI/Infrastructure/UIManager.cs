using System;
using Infrastructure;
using Infrastructure.DI;
using UnityEngine;
using UnityEngine.EventSystems;
using CharacterController = Character.CharacterController;

namespace UI.Infrastructure
{
    public sealed class UIManager : MonoBehaviour
    {
        [InjectCustom]
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        
        [SerializeField] private MainMenuScreen _mainMenuScreen;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private HUDScreen _hud;

        private Action _mainMenuShowHandler;
        private Action _hudShowHandler;
        private Action _hudHideHandler;
        private Action _loseScreenShowHandler;
        private Action _pauseScreenShowHandler;

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            _mainMenuShowHandler = () => ShowScreen(_mainMenuScreen);
            _hudShowHandler = () => ShowScreen(_hud);
            _hudHideHandler = () => CloseScreen(_hud);
            _loseScreenShowHandler = () => ShowScreen(_loseScreen);
            _pauseScreenShowHandler = () => ShowScreen(_pauseScreen);
            
            _gameBootstrapper.Game.StateMachine.GetState<MainMenuState>().OnMainMenuSceneLoaded += _mainMenuShowHandler;
            
            _gameBootstrapper.Game.StateMachine.GetState<MainMenuState>().OnMainMenuState += _hudHideHandler;
            _gameBootstrapper.Game.StateMachine.GetState<LoadInGameState>().OnGameLoopSceneLoaded += _hudShowHandler;
            
            _gameBootstrapper.Game.StateMachine.GetState<LoseState>().OnLoseState += _loseScreenShowHandler;
            _gameBootstrapper.Game.StateMachine.GetState<PauseState>().OnPauseState += _pauseScreenShowHandler;
        }
        
        private void OnDisable()
        {
            _gameBootstrapper.Game.StateMachine.GetState<MainMenuState>().OnMainMenuSceneLoaded -= _mainMenuShowHandler;
            
            _gameBootstrapper.Game.StateMachine.GetState<MainMenuState>().OnMainMenuState -= _hudHideHandler;
            _gameBootstrapper.Game.StateMachine.GetState<LoadInGameState>().OnGameLoopSceneLoaded -= _hudShowHandler;
            
            _gameBootstrapper.Game.StateMachine.GetState<LoseState>().OnLoseState -= _loseScreenShowHandler;
            _gameBootstrapper.Game.StateMachine.GetState<PauseState>().OnPauseState -= _pauseScreenShowHandler;
        }
        
        private void ShowScreen(Component screen)
        {
            screen.gameObject.SetActive(true);
        }

        public void CloseScreen(Component screen)
        {
            EventSystem.current.SetSelectedGameObject(null);
            screen.gameObject.SetActive(false);
        }
    }
}