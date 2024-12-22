using CodeBase.Infrastructure;
using UnityEngine;

namespace Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    public Game Game;

    private void Awake()
    {
      this.Game = new Game(this);
      this.Game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}