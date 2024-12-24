using System;
using System.Collections;
using CodeBase.Infrastructure;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure
{
  public class SceneLoader
  {
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner)
    {
      _coroutineRunner = coroutineRunner;
    }

    public void Load(string name, Action onLoaded = null, Action onLoadStart = null) =>
      _coroutineRunner.StartCoroutine(LoadSceneAsync(name, onLoaded, onLoadStart));
    
    public void ReLoad(string name, Action onLoaded = null, Action onLoadStart = null) =>
      _coroutineRunner.StartCoroutine(ReLoadSceneAsync(name, onLoaded, onLoadStart));

    private IEnumerator LoadSceneAsync(string nextScene, Action onLoaded, Action onLoadStart)
    {
      if (SceneManager.GetActiveScene().name == nextScene)
      {
        onLoaded?.Invoke();
        yield break;
      }
      
      onLoadStart?.Invoke();

      yield return new WaitForSeconds(1f);
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene, LoadSceneMode.Single);

      while (!waitNextScene.isDone)
        yield return null;

      onLoaded?.Invoke();
    }
    
    private IEnumerator ReLoadSceneAsync(string nextScene, Action onLoaded, Action onLoadStart)
    {
      onLoadStart?.Invoke();
      
      yield return new WaitForSeconds(1f);
      
      AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);

      while (!waitNextScene.isDone)
        yield return null;
      
      onLoaded?.Invoke();
    }
  }
}