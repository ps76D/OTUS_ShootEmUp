using System.Collections.Generic;
using Bullets;
using Infrastructure.CommonInterfaces;
using Infrastructure.Listeners;
using Input;
using Level;
using UI.Infrastructure;
using UnityEngine;
using Zenject;
using CharacterController = Character.CharacterController;

namespace Infrastructure.DI
{
    public class GameSceneSystemsInstaller : MonoInstaller
    {
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private InputConfig _inputConfig;

        [SerializeField] private LevelBounds _levelBounds;
        
        private InputManager _inputManager;

        public override void InstallBindings()
        {
            _inputManager = new InputManager(_inputConfig);
            
            BindObject(_characterController);
            
            Container.Bind<InputManager>().FromInstance(_inputManager).AsCached().NonLazy();

            BindObject(_levelBounds);
            
            BindInterfaces();
        }

        private void BindObject<T>(T obj)
        {
            if (obj != null)
            {
                Container.Bind<T>()
                    .FromInstance(obj)
                    .AsSingle().NonLazy();
            }
        }

        private void BindInterfaces()
        {
            Container.Bind<IUpdatable>().FromInstance(_inputManager).AsCached().NonLazy();
            
            Container.BindInterfacesAndSelfTo<IFixedUpdatable>().FromComponentsInHierarchy().AsTransient().NonLazy();
            
            Debug.Log("BindInterfaces Game Scene");
        }
    }
}