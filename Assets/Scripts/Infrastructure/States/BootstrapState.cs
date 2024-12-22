using UnityEngine;

namespace Infrastructure
{
  public class BootstrapState : IState
  {
    private const string Initial = "Initial";
    private const string UI = "UI";
    private const string Game = "Game";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      this._stateMachine = stateMachine;
      this._sceneLoader = sceneLoader;
    }

    public void Enter()
    {
      /*this.RegisterServices();*/
      this._sceneLoader.Load(Initial, onLoaded: this.EnterLoadLevel);
      Debug.Log("Enter BootstrapState");
    }

    public void Exit()
    {
    }

    private void EnterLoadLevel() => 
      this._stateMachine.Enter<LoadLevelState, string>(UI);

    /*private void RegisterServices()
    {

    }*/
  }
}