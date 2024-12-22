using Infrastructure.DI;
using UI.Infrastructure;
using UI.Interfaces;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public sealed class PauseScreen : UIScreen, IPauseGameListener
    {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _exitButton;
        
        public void Start()
        {
            this._resumeButton.onClick.AddListener(this.ResumeGame);
            this._exitButton.onClick.AddListener(this.ResumeGame);
        }
        
        private void ResumeGame()
        {
            UIManager.CloseScreen(this);
        }
        
        private void ExitGame()
        {
            this._uiManager.ExitGame(this);
        }
    }
}