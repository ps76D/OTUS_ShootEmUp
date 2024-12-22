using System;
using System.Collections.Generic;
using Infrastructure;
using Infrastructure.DI;
using UnityEngine;

namespace UI.Infrastructure
{
    public sealed class UIInstaller : MonoBehaviour
    {
        /*private static UIInstaller _instance;*/
        
        [SerializeField] private UIManager _uiManager;
        [SerializeField] private GameBootstrapper _gameBootstrapper;
        
        private void Awake()
        {
            /*_instance = this;*/

            this._gameBootstrapper = FindObjectOfType<GameBootstrapper>();
            this._uiManager = this.GetComponent<UIManager>();
        }

        private void Start()
        {
            ServiceLocator.AddService(typeof(GameBootstrapper), this._gameBootstrapper);
            ServiceLocator.AddService(typeof(UIManager), this._uiManager);

            var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.InjectObject(monoBehaviour);
            }
        }
    }
}