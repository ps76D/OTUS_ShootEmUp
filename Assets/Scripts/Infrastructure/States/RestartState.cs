using System;
using GameManager;
using UI.Infrastructure;
using UnityEngine;

namespace Infrastructure
{
  public class RestartState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    

    public RestartState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Exit()
    {

    }

    public void Enter()
    {
      _sceneLoader.ReLoad(SceneNamesConsts.Game, OnLoaded);

      Debug.Log("Enter RestartState");
    }
    
    private void OnLoaded()
    {
      _stateMachine.Enter<GameLoopState>();
    }
  }
}