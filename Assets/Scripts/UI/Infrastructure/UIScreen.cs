using Infrastructure.DI;
using Infrastructure.Listeners;
using UI.Interfaces;
using UnityEngine;

namespace UI.Infrastructure
{
    public abstract class UIScreen : MonoBehaviour, IGameStateListener, IUIScreen
    {
        [InjectCustom]
        [SerializeField] protected UIManager _uiManager;
        
        /*private void Start()
        {
            this._uiManager = ServiceLocator.GetListeners<IUIScreen>();
        }*/

    }
}