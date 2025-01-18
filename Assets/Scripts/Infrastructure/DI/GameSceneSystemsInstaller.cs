using System;
using System.Collections.Generic;
using Bullets;
using Components;
using Enemy;
using Infrastructure.CommonInterfaces;
using Infrastructure.Listeners;
using Input;
using Level;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;
using CharacterController = Character.CharacterController;

namespace Infrastructure.DI
{
    public class GameSceneSystemsInstaller : MonoInstaller
    {
        [SerializeField] private UpdateController _updateController;
        
        [SerializeField] private InputConfig _inputConfig;
        [SerializeField] private BackgroundConfig _backgroundConfig;

        [SerializeField] private HitPointsComponent _character;

        [SerializeField] private LevelBounds _levelBounds;
        
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private BulletPool _bulletPool;

        [SerializeField] private Transform _worldTransform;
        
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private GameObject _enemyPrefab;
        [SerializeField] private EnemyPositionsProvider _enemyPositionsProvider;

        private InputManager _inputManager;
        private LevelBackgroundMover _levelBackgroundMover;
        private CharacterController _characterController;
        
        public override void InstallBindings()
        {
            _inputManager = new InputManager(_inputConfig);
            _levelBackgroundMover = new LevelBackgroundMover(_backgroundConfig, _levelBounds);
            _characterController = new CharacterController(_character);

            Container.Bind<InputManager>().FromInstance(_inputManager).AsCached().NonLazy();
            Container.Bind<UpdateController>().FromInstance(_updateController).AsCached().NonLazy();
            
            Container.Bind<EnemyManager>().ToSelf().AsSingle().WithArguments(_enemyPool, _enemyPrefab, _worldTransform, _enemyPositionsProvider, _character.transform);
            
            Container.Bind<BulletManager>().ToSelf().AsSingle().WithArguments(_bulletPrefab, _bulletPool, _worldTransform, _updateController);

            BindObject(_characterController);
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
            Container.Bind<IFixedUpdatable>().FromInstance(_levelBackgroundMover).AsCached().NonLazy();
            
            Container.BindInterfacesAndSelfTo<IFixedUpdatable>().FromComponentsInHierarchy().AsTransient().NonLazy();
            
            Debug.Log("BindInterfaces Game Scene");
        }
    }
}