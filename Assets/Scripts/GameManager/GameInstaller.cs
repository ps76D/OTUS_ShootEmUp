using System.Collections.Generic;
using Infrastructure;
using Infrastructure.DI;
using Infrastructure.Listeners;
using Input;
using Level;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;
using CharacterController = Character.CharacterController;

namespace GameManager
{
    public class GameInstaller : MonoBehaviour
    {
        private InGameServiceLocator _serviceLocator;
        private InGameDependencyInjector _dependencyInjector;
        private MonoBehaviour[] _allMonoBehaviours;
        
        private IGameStateListener[] _gameStateListeners;

        private void Awake()
        {
            _serviceLocator = new InGameServiceLocator();
            _dependencyInjector = new InGameDependencyInjector(_serviceLocator);
            
            _serviceLocator.AddListeners<IGameStateListener>(GetAllGameStateListeners());
            
            _allMonoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            
            InjectLocal();
        }
        
        private IEnumerable<IGameStateListener> GetAllGameStateListeners()
        {
            if (_gameStateListeners != null) return _gameStateListeners;
            _gameStateListeners = FindObjectsOfInterface<IGameStateListener>();
            return _gameStateListeners;
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
        
        private void InjectLocal()
        {
            Debug.Log("Inject Game Objects");
            
            foreach(MonoBehaviour monoBehaviour in _allMonoBehaviours)
            {
                _dependencyInjector.InjectLocal(monoBehaviour);
            }
            Debug.Log("Inject Game Listeners");
        }
    }
}