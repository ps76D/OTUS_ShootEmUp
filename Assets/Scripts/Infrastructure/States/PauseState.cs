using System;
using UnityEngine;

namespace Infrastructure
{
  public class PauseState : IState
  {
    public event Action OnPause;
    public PauseState(GameStateMachine stateMachine)
    {
    }

    public void Exit()
    {
    }

    public void Enter()
    {
      OnPause?.Invoke();
      Debug.Log("Enter PauseState");
    }
  }
}