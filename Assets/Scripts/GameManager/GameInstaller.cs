using System.Linq;
using Infrastructure;
using Infrastructure.DI;
using UI.Infrastructure;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameInstaller : MonoBehaviour
    {
        /*[SerializeField] private GameBootstrapper _gameBootstrapper;*/
        
        private void Awake()
        {
            /*_instance = this;*/

            Inject();
        }
        
        private void Inject()
        {
            var allMonoBehaviours = FindObjectsInActiveScene<MonoBehaviour>();
            
            foreach(MonoBehaviour monoBehaviour in allMonoBehaviours)
            {
                DependencyInjector.InjectObject(monoBehaviour);

            }
            
            Debug.Log("Inject Game Objects");
        }

        private T[] FindObjectsInActiveScene<T>() where T : Object
        {
            Scene activeScene = SceneManager.GetActiveScene();
            var rootObjects = activeScene.GetRootGameObjects();

            return rootObjects
                .SelectMany(go => go.GetComponentsInChildren<T>(true))
                .ToArray();
        }
    }
}