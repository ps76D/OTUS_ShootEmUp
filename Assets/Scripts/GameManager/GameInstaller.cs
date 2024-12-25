using Infrastructure;
using Infrastructure.DI;
using UnityEngine;
using CharacterController = Character.CharacterController;

namespace GameManager
{
    public class GameInstaller : MonoBehaviour
    {
        [SerializeField] private CharacterController _character;

        private InGameServiceLocator _serviceLocator;
        private InGameDependencyInjector _dependencyInjector;

        private void Awake()
        {
            _serviceLocator = new InGameServiceLocator();
            _dependencyInjector = new InGameDependencyInjector(_serviceLocator);
            
            _serviceLocator.AddService(typeof(CharacterController), _character);

            Inject();
        }
        
        private void Inject()
        {
            var allMonoBehaviours = FindObjectsOfType<MonoBehaviour>(true);
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                _dependencyInjector.InjectLocalObject(monoBehaviour);
            }
            
            Debug.Log("Inject Game Objects");
        }
    }
}