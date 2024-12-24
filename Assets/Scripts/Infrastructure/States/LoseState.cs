using System;
using GameManager;
using UnityEngine;

namespace Infrastructure
{
  public class LoseState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    
    public event Action OnLoseState;
    
    public LoseState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Exit()
    {
    }

    public void Enter()
    {
      OnLoseState?.Invoke();

      /*TimeManager.StopTime(true);*/
      
      Debug.Log("Enter LoseState");
    }
  }
}