using UnityEngine;

namespace Infrastructure
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }
    
    public void Enter(string sceneName)
    {
      _sceneLoader.Load(sceneName, OnLoaded);
      
      Debug.Log("Enter LoadLevelState");
    }

    public void Exit()
    {
    }

    private void OnLoaded()
    {
      _stateMachine.Enter<MainMenuState>();
    }
  }
}