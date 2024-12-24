using UI.Infrastructure;
using UnityEngine;
using Zenject;

namespace Infrastructure.DI
{
    public class CommonSystemsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            GameBootstrapper gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            UIManager uiManager = FindObjectOfType<UIManager>();

            BindObjectAsSingleNonLazy(gameBootstrapper);
            BindObjectAsSingleNonLazy(uiManager);
            
            
            /*Container.Bind<GameBootstrapper>().FromInstance(gameBootstrapper).AsSingle().NonLazy();
            
            Container.Bind<UIManager>().FromInstance(uiManager).AsSingle().NonLazy();*/

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