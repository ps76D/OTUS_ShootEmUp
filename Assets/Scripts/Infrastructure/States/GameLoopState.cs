using System;
using GameManager;
using UnityEngine;

namespace Infrastructure
{
  public class GameLoopState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    
    public static event Action OnGameLoopState;
    
    private const string MainMenu = "MainMenu";
    private const string Game = "Game";
    
    public GameLoopState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      this._stateMachine = gameStateMachine;
      this._sceneLoader = sceneLoader;
    }

    public void Exit()
    {
      this._sceneLoader.Load(MainMenu);
    }

    public void Enter()
    {
      this._sceneLoader.Load(Game);
      
      TimeManager.StopTime(false);
      
      Debug.Log("Enter GameLoopState");
    }
  }
}