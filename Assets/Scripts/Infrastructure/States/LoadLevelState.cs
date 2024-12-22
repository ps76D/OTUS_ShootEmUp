using UnityEngine;

namespace Infrastructure
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      this._stateMachine = gameStateMachine;
      this._sceneLoader = sceneLoader;
    }
    
    public void Enter(string sceneName)
    {
      this._sceneLoader.Load(sceneName, this.OnLoaded);
      Debug.Log("Enter LoadLevelState");
    }

    public void Exit()
    {
    }

    private void OnLoaded()
    {
      this._stateMachine.Enter<MainMenuState>();
    }
  }
}