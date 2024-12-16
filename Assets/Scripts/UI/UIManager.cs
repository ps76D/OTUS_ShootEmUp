using UnityEngine;
using CharacterController = Character.CharacterController;

namespace UI
{
    public sealed class UIManager : MonoBehaviour
    {
        [SerializeField] private LoseScreen _loseScreen;

        private void Awake()
        {
            CloseScreen(this._loseScreen);
        }

        private void OnEnable()
        {
            CharacterController.OnCharacterDeath += this.ShowLoseScreen;
        }
        
        private void OnDisable()
        {
            CharacterController.OnCharacterDeath -= this.ShowLoseScreen;
        }
        
        private void ShowLoseScreen()
        {
            ShowScreen(this._loseScreen);
        }

        public static void ShowScreen(UIScreen screen)
        {
            screen.gameObject.SetActive(true);
        }

        public static void CloseScreen(UIScreen screen)
        {
            screen.gameObject.SetActive(false);
        }
    }
}