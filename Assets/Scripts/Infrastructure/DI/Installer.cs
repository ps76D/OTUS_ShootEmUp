using System.Collections;
using System.Collections.Generic;
using GameManager.Listeners;
using Infrastructure.Listeners;
using UI;
using UI.Infrastructure;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace Infrastructure.DI
{
    public sealed class Installer : MonoBehaviour
    {
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameBootstrapper _gameBootstrapper;

        
        private IUpdatableListener[] _updatableItems;
        private IGameStateListener[] _gameStateListeners;
        private IGameListener[] _gameListeners;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            
            _gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            _uiManager = FindObjectOfType<UIManager>();
            
            ServiceLocator.AddService(typeof(GameBootstrapper), _gameBootstrapper);
            ServiceLocator.AddService(typeof(UIManager), _uiManager);
            
            ServiceLocator.AddListeners<IGameStateListener>(GetAllGameStateListeners());
            ServiceLocator.AddListeners<IGameListener>(GetAllInGameListeners());

            Inject();
        }

        private IEnumerable<IGameStateListener> GetAllGameStateListeners()
        {
            if (_gameStateListeners != null) return _gameStateListeners;
            _gameStateListeners = FindObjectsOfInterface<IGameStateListener>();
            return _gameStateListeners;
        }
        
        private IEnumerable<IGameListener> GetAllInGameListeners()
        {
            if (_gameListeners != null) return _gameListeners;
            _gameListeners = FindObjectsOfInterface<IGameListener>();
            return _gameListeners;
        }

        private void Inject()
        {
            var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.InjectObject(monoBehaviour);
            }
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.Inject(monoBehaviour);
            }
            
            Debug.Log("Inject Common Systems");
        }

        private T[] FindObjectsOfInterface<T>() where T : class
        {
            var monoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            int capacity = 0;

            for (int i = monoBehaviours.Length - 1; i >= 0; i--)
            {
                MonoBehaviour mb = monoBehaviours[i];
                if (mb is T)
                {
                    capacity++;
                }
            }

            var result = new T[capacity];
            int index = 0;

            for (int i = monoBehaviours.Length - 1; i >= 0; i--)
            {
                MonoBehaviour mb = monoBehaviours[i];
                if (mb is T t)
                {
                    result[index++] = t;
                }
            }

            return result;
        }
    }
}