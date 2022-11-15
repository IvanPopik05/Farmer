using System;
using System.Collections.Generic;
using Player_container;
using UnityEngine;

namespace States
{
    public class PlayerStateMachine
    {
        public IState CurrentState { get; set; }

        private readonly Dictionary<Type, IState> _states;
        public PlayerStateMachine(Player player, Toilet toilet, Transform restZone)
        {
            _states = new Dictionary<Type, IState>()
            {
                [typeof(PlayerIdleState)] = new PlayerIdleState(this),
                [typeof(PlayerGoingToRestState)] = new PlayerGoingToRestState(this, player,restZone),
                [typeof(PlayerGoingToToiletState)] = new PlayerGoingToToiletState(this, player,toilet),
            };
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        private TState ChangeState<TState>() where TState : class, IState
        {
            CurrentState?.Exit();
        
            TState state = GetState<TState>();
            CurrentState = state;
        
            return state;
        }

        public void Update() => 
            CurrentState.Update();

        private TState GetState<TState>() where TState : class, IState
        {
            return _states[typeof(TState)] as TState;
        }
    }
}