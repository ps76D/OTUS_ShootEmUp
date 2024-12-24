using System.Collections.Generic;
using Infrastructure.Listeners;
using UI;
using UI.Infrastructure;
using UnityEngine;

namespace Infrastructure.DI
{
    public sealed class Installer : MonoBehaviour
    {
        // private static Installer _instance;

        private IUpdatableListener[] _updatableItems;
        private IGameStateListener[] _gameStateListeners;
        /*private GameBootstrapper[] _gameBootstrappers;*/
        
        /*private IUserInputListener[] userInputListeners = null;*/
        
        /*private IEnumerable<IUpdatableListener> GetAllUpdatableItems()
        {
            if (_instance._updatableItems == null)
                _instance._updatableItems = _instance.GetComponentsInChildren<IUpdatableListener>();
            return _instance._updatableItems;
        }*/

        private IEnumerable<IGameStateListener> GetAllGameStateListeners()
        {
            if (_gameStateListeners == null)
                _gameStateListeners = GetComponentsInChildren<IGameStateListener>();
            return _gameStateListeners;
        }
        
        /*private IEnumerable<GameBootstrapper> GetGameBootstrapper()
        {
            if (_gameBootstrappers == null)
                _gameBootstrappers = GetComponentsInChildren<GameBootstrapper>();
            return _gameBootstrappers;
        }*/

        /*public IEnumerable<IUserInputListener> GetAllUserInputListeners()
        {
            if (userInputListeners == null)
                userInputListeners = GetComponentsInChildren<IUserInputListener>();
            return userInputListeners;
        }*/

        private void Awake()
        {
            DontDestroyOnLoad(this);
            // _instance = this;
            /*ServiceLocator.AddListeners<IUpdatableListener>(_instance.GetAllUpdatableItems());*/
            ServiceLocator.AddListeners<IGameStateListener>(GetAllGameStateListeners());
            /*ServiceLocator.AddListeners<GameBootstrapper>(_instance.GetGameBootstrapper());*/
            
            /*ServiceLocator.AddListeners<IUserInputListener>(instance.GetAllUserInputListeners());*/

            var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>();
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.Inject(monoBehaviour);
            }
        }
    }
}