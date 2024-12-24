using System;
using Components;
using Infrastructure;
using Infrastructure.DI;
using Input;
using UnityEngine;
using Zenject;
using CharacterController = Character.CharacterController;

namespace GameManager
{
    public sealed class GameManager : MonoBehaviour
    {
        [Inject]
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        
        private CharacterController _characterController;
        
        private InputManager _inputManager;

        private GameStateMachine _gameStateMachine;
        
        private void Awake()
        {
            _characterController = FindObjectOfType<CharacterController>();
            _inputManager = FindObjectOfType<InputManager>();
        }

        private void OnEnable()
        {
            _characterController.OnCharacterDeath += FinishGame;
            
            //TODO Переписать кусок ниже
            UI.LoseScreen.OnReviveButtonClicked += Revive;
        }
        
        private void OnDisable()
        {
            _characterController.OnCharacterDeath -= FinishGame;
            
            //TODO Переписать кусок ниже
            UI.LoseScreen.OnReviveButtonClicked -= Revive;
        }

        private void Start()
        {
            _gameStateMachine = _gameBootstrapper.Game.StateMachine;
            Debug.Log("GameManager Started");
        }

        private void Revive()
        {
            HitPointsComponent character = _characterController.Character;
            character.Revive();
            
            EnablePlayerInput(true);

            /*TimeManager.StopTime(false);*/
        }

        private void FinishGame()
        {
            _gameStateMachine.Enter<LoseState>();
            
            Debug.Log("Game over!");
            
            EnablePlayerInput(false);
        }

        private void EnablePlayerInput(bool value)
        {
            _inputManager.gameObject.SetActive(value);
        }
    }
}