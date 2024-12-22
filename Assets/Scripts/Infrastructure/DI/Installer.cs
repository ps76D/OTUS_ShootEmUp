using System.Collections.Generic;
using UI;
using UI.Infrastructure;
using UnityEngine;

namespace Infrastructure.DI
{
    public sealed class Installer : MonoBehaviour
    {
        private static Installer _instance;

        private IUpdatableListener[] _updatableItems;
        private IGameStateListener[] _gameStateListeners;
        
        [SerializeField] private UIManager _uiManager;
        
        /*private IUserInputListener[] userInputListeners = null;*/
        
        private IEnumerable<IUpdatableListener> GetAllUpdatableItems()
        {
            if (_instance._updatableItems == null)
                _instance._updatableItems = _instance.GetComponentsInChildren<IUpdatableListener>();
            return _instance._updatableItems;
        }

        private IEnumerable<IGameStateListener> GetAllGameStateListeners()
        {
            if (this._gameStateListeners == null)
                this._gameStateListeners = this.GetComponentsInChildren<IGameStateListener>();
            return this._gameStateListeners;
        }

        /*public IEnumerable<IUserInputListener> GetAllUserInputListeners()
        {
            if (userInputListeners == null)
                userInputListeners = GetComponentsInChildren<IUserInputListener>();
            return userInputListeners;
        }*/

        private void Awake()
        {
            _instance = this;
            ServiceLocator.AddListeners<IUpdatableListener>(_instance.GetAllUpdatableItems());
            ServiceLocator.AddListeners<IGameStateListener>(_instance.GetAllGameStateListeners());
            
            /*ServiceLocator.AddListeners<IUserInputListener>(instance.GetAllUserInputListeners());*/

            var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>();
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.Inject(monoBehaviour);
            }
        }
    }
}