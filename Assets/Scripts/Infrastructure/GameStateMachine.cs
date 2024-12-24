using System;
using System.Collections.Generic;

namespace Infrastructure
{
  public class GameStateMachine
  {
    private readonly Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;
    
    public GameStateMachine(SceneLoader sceneLoader)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
        [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader),
        [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader),
        [typeof(RestartState)] = new RestartState(this, sceneLoader),
        [typeof(PauseState)] = new PauseState(this, sceneLoader),
        [typeof(LoseState)] = new LoseState(this, sceneLoader),
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();
      
      TState state = GetState<TState>();
      _activeState = state;
      
      return state;
    }

    public TState GetState<TState>() where TState : class, IExitableState => 
      _states[typeof(TState)] as TState;
  }
}