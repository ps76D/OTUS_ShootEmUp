using System;
using UnityEngine;

namespace Infrastructure
{
  public class LoseState : IState
  {
    public event Action OnLose;
    public LoseState(GameStateMachine stateMachine)
    {
    }

    public void Exit()
    {
    }

    public void Enter()
    {
      OnLose?.Invoke();
      Debug.Log("Enter LoseState");
    }
  }
}