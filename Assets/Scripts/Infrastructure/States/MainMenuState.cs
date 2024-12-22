using System;
using GameManager;
using UI.Infrastructure;
using UnityEngine;

namespace Infrastructure
{
  public class MainMenuState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;


    public static event Action OnMainMenu;
    
    private const string MainMenu = "MainMenu";
    private const string Game = "Game";
    
    public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      this._stateMachine = gameStateMachine;
      this._sceneLoader = sceneLoader;
    }

    public void Exit()
    {
    }

    public void Enter()
    {
      this._sceneLoader.Load(MainMenu);
      
      OnMainMenu?.Invoke();
      
      TimeManager.StopTime(true);
      
      Debug.Log("Enter MainMenuState");
    }
  }
}