using System.Collections.Generic;
using System.Linq;
using Infrastructure.Listeners;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.DI
{
    public class CommonSystemsInstaller : MonoInstaller
    {
        private List<IGameStateListener> _gameStateListeners;
        
        public override void InstallBindings()
        {
            GameBootstrapper gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            UIManager uiManager = FindObjectOfType<UIManager>();

            BindObjectAsSingleNonLazy(gameBootstrapper);
            BindObjectAsSingleNonLazy(uiManager);
        }

        private void BindObjectAsSingleNonLazy<T>(T obj)
        {
            if (obj != null)
            {
                Container.Bind<T>().FromInstance(obj).AsSingle().NonLazy();
            }
            else
            {
                Debug.LogError( "Binding Object not found");
            } 
        }
    }
}