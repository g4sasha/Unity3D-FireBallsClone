using System;
using System.Collections.Generic;

namespace Core
{
    public class StateMachine<T>
        where T : GameState
    {
        public T CurrentState => _currentState;
        private readonly Dictionary<Type, T> _states = new();
        private T _currentState;

        public StateMachine(params T[] states)
        {
            foreach (var state in states)
            {
                _states[state.GetType()] = state;
            }
        }

        public void ChangeState<U>()
            where U : T
        {
            _currentState?.Exit();

            if (_states.TryGetValue(typeof(U), out var newState))
            {
                _currentState = newState;
                _currentState.Enter();
            }
            else
            {
                throw new InvalidOperationException(
                    $"State of type {typeof(U).Name} is not initialized."
                );
            }
        }
    }
}
