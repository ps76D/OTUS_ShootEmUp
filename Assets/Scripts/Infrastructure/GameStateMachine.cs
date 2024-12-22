using System;
using System.Collections.Generic;

namespace Infrastructure
{
  public class GameStateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public GameStateMachine(SceneLoader sceneLoader)
    {
      this._states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
        [typeof(MainMenuState)] = new MainMenuState(this, sceneLoader),
        [typeof(GameLoopState)] = new GameLoopState(this, sceneLoader),
        [typeof(PauseState)] = new PauseState(this),
        [typeof(LoseState)] = new LoseState(this),
      };
    }
    
    public void Enter<TState>() where TState : class, IState
    {
      IState state = this.ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = this.ChangeState<TState>();
      state.Enter(payload);
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      this._activeState?.Exit();
      
      TState state = this.GetState<TState>();
      this._activeState = state;
      
      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState => 
      this._states[typeof(TState)] as TState;
  }
}