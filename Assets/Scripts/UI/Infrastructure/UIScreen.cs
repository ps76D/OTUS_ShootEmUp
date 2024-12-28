using Infrastructure.DI;
using Infrastructure.Listeners;
using UnityEngine;

namespace UI.Infrastructure
{
    public abstract class UIScreen : MonoBehaviour, IGameStateListener, IUIScreen
    {
        [InjectCustom]
        [SerializeField] protected UIManager _uiManager;
    }
}