using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.DI;
using Infrastructure.Listeners;
using UnityEngine;

namespace UI.Infrastructure
{
    public sealed class UIInstaller : MonoBehaviour
    {
        /*private static UIInstaller _instance;*/
        
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        
        /*private IGameStateListener[] _gameStateListeners;*/
        
        private void Awake()
        {
            /*_instance = this;*/

            _gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            _uiManager = GetComponent<UIManager>();
            
            ServiceLocator.AddService(typeof(GameBootstrapper), _gameBootstrapper);
            ServiceLocator.AddService(typeof(UIManager), _uiManager);
            
            /*ServiceLocator.AddListeners<IGameStateListener>(GetAllGameStateListeners());*/

            Inject();
        }
        
        /*private IEnumerable<IGameStateListener> GetAllGameStateListeners()
        {
            if (_gameStateListeners == null)
                _gameStateListeners = GetComponentsInChildren<IGameStateListener>();
            return _gameStateListeners;
        }*/

        private void Inject()
        {
            var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.InjectObject(monoBehaviour);
            }
            
            /*foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.Inject(monoBehaviour);
            }*/
            
            Debug.Log("Inject UI Objects");
        }
    }
}