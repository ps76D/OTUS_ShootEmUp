using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public sealed class LoseScreen : UIScreen
    {
        [SerializeField] private Button _restartButton;
        
        public static event Action OnRestartButtonClicked;
        
        public void Start()
        {
            this._restartButton.onClick.AddListener(this.CloseLoseScreen);
        }
        
        private void CloseLoseScreen()
        {
            OnRestartButtonClicked?.Invoke();
            EventSystem.current.SetSelectedGameObject(null);
            UIManager.CloseScreen(this);
        }
    }
}