using CodeBase.Infrastructure;
using UI;
using UI.Infrastructure;
using UnityEngine;

namespace Infrastructure
{
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
  {
    [SerializeField] private LoadingCurtain _loadingCurtain;
    
    public Game Game;

    private void Awake()
    {
      Game = new Game(this, _loadingCurtain);
      Game.StateMachine.Enter<BootstrapState>();

      DontDestroyOnLoad(this);
    }
  }
}