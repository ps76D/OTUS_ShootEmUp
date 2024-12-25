using System.Collections.Generic;
using Infrastructure;
using Infrastructure.DI;
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
        [SerializeField] private CharacterController _character;
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private LevelBounds _levelBounds;

        private InGameServiceLocator _serviceLocator;
        private InGameDependencyInjector _dependencyInjector;

        private void Awake()
        {
            _serviceLocator = new InGameServiceLocator();
            _dependencyInjector = new InGameDependencyInjector(_serviceLocator);
            
            _serviceLocator.AddService(typeof(CharacterController), _character);
            _serviceLocator.AddService(typeof(InputManager), _inputManager);
            _serviceLocator.AddService(typeof(LevelBounds), _levelBounds);
            
            InjectCommon();
            InjectLocal();
        }
        
        private void InjectLocal()
        {
            var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                _dependencyInjector.InjectLocalObject(monoBehaviour);
            }
            
            Debug.Log("Inject Game Objects");
        }
        
        private void InjectCommon()
        {
            var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.InjectObject(monoBehaviour);
            }
            
            Debug.Log("Inject Common Objects in Scene");
        }
    }
}