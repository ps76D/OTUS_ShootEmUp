using CodeBase.Infrastructure;
using UI.Infrastructure;
using UnityEngine;

namespace Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public Game Game;

    private void Awake()
    {
      Game = new Game(this);
      Game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}