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
    
    public event Action OnMainMenu;

    public MainMenuState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Exit()
    {
      _sceneLoader.Load(SceneNamesConsts.Game);
    }

    public void Enter()
    {
      _sceneLoader.Load(SceneNamesConsts.MainMenu);
      
      OnMainMenu?.Invoke();
      
      /*TimeManager.StopTime(true);*/
      
      Debug.Log("Enter MainMenuState");
    }
  }
}