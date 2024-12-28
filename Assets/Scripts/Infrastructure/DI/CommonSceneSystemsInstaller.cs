using System.Collections.Generic;
using Infrastructure.Listeners;
using Input;
using Level;
using UI;
using UI.Infrastructure;
using UnityEngine;
using Zenject;
using CharacterController = Character.CharacterController;

namespace Infrastructure.DI
{
    public class CommonSceneSystemsInstaller : MonoInstaller
    {
        /*[SerializeField] private CharacterController _characterController;

        [SerializeField] private InputManager _inputManager;
        [SerializeField] private LevelBounds _levelBounds;*/
        public override void InstallBindings()
        {
            /*BindObject(_characterController);
            BindObject(_inputManager);
            BindObject(_levelBounds);*/
            
            /*BindInterfaces();*/
        }

        /*private void BindObject<T>(T obj)
        {
            if (obj != null)
            {
                Container.Bind<T>()
                    .FromInstance(obj)
                    .AsSingle().NonLazy();
            }
        }*/

        private void BindInterfaces()
        {
            /*Container.Bind<IGameStateListener>().To<StartCountdownWidget>().AsTransient();*/
            /*Container.Bind<IGameStateListener>().To<IPauseGameListener>().AsTransient();
            Container.Bind<IGameStateListener>().To<IFinishGameListener>().AsTransient();*/

            Container.Bind<IEnumerable<IGameStateListener>>().FromResolveAll().AsSingle().NonLazy();
            
            Debug.Log("Bind IEnumerable<IGameStateListener> ");
        }
    }
}