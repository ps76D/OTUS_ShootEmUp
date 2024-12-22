using CodeBase.Infrastructure;

namespace Infrastructure
{
  public class Game
  {
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner)
    {
      this.StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
    }
  }
}