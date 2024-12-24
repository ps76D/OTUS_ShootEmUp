using System;
using GameManager;
using UnityEngine;

namespace Infrastructure
{
  public class GameLoopState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    
    public event Action OnGameLoopState;

    public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Exit()
    {
    }

    public void Enter()
    {
      OnGameLoopState?.Invoke();
      
      /*TimeManager.StopTime(false);*/
      
      Debug.Log("Enter GameLoopState");
    }
  }
}