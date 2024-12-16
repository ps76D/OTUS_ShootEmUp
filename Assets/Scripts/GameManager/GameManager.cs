using Components;
using Input;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace GameManager
{
    public sealed class GameManager : MonoBehaviour
    {
        private CharacterController _characterController;
        
        private InputManager _inputManager;
        
        private void Awake()
        {
            this._characterController = FindObjectOfType<CharacterController>();
            this._inputManager = FindObjectOfType<InputManager>();
        }

        private void OnEnable()
        {
            CharacterController.OnCharacterDeath += this.FinishGame;
            UI.LoseScreen.OnRestartButtonClicked += this.Revive;
        }

        private void Revive()
        {
            HitPointsComponent character = this._characterController.Character;
            character.Revive();
            
            this.EnablePlayerInput(true);

            this.StopTime(false);
        }

        private void FinishGame()
        {
            Debug.Log("Game over!");
            
            this.EnablePlayerInput(false);
            
            this.StopTime(true);
        }

        private void EnablePlayerInput(bool value)
        {
            this._inputManager.gameObject.SetActive(value);
        }

        private void StopTime(bool value)
        {
            Time.timeScale = value ? 0 : 1;
        }
    }
}