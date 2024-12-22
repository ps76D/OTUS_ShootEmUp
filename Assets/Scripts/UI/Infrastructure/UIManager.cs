using System;
using Infrastructure;
using UnityEngine;
using UnityEngine.EventSystems;
using CharacterController = Character.CharacterController;

namespace UI.Infrastructure
{
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField] private MainMenuScreen _mainMenuScreen;
        [SerializeField] private LoseScreen _loseScreen;
        [SerializeField] private PauseScreen _pauseScreen;
        [SerializeField] private HUDScreen _hud;

        private readonly Action _mainMenuShowHandler;

        public UIManager()
        {
            this._mainMenuShowHandler = () => ShowScreen(this._mainMenuScreen);
        }

        private void Awake()
        {
            DontDestroyOnLoad(this);
        }

        private void OnEnable()
        {
            MainMenuState.OnMainMenu += this._mainMenuShowHandler;
            
            CharacterController.OnCharacterDeath += this.ShowLoseScreen;
            
        }
        
        private void OnDisable()
        {
            MainMenuState.OnMainMenu -= this._mainMenuShowHandler;
            
            CharacterController.OnCharacterDeath -= this.ShowLoseScreen;
        }
        
        private void ShowLoseScreen()
        {
            ShowScreen(this._loseScreen);
        }

        private static void ShowScreen(Component screen)
        {
            screen.gameObject.SetActive(true);
        }

        public static void CloseScreen(Component screen)
        {
            EventSystem.current.SetSelectedGameObject(null);
            screen.gameObject.SetActive(false);
        }
        
        public void ExitGame(UIScreen screen)
        {
            CloseScreen(screen);
            this.ShowMainMenuScreen();
        }
        
        private void ShowMainMenuScreen()
        {
            ShowScreen(this._mainMenuScreen);
        }
    }
}