using Infrastructure.DI;
using UI.Interfaces;
using UnityEngine;

namespace UI.Infrastructure
{
    public abstract class UIScreen : MonoBehaviour, IGameStateListener, IUIScreen
    {
        [Inject]
        [SerializeField] internal UIManager _uiManager;
        
        /*private void Start()
        {
            this._uiManager = ServiceLocator.GetListeners<IUIScreen>();
        }*/

    }
}