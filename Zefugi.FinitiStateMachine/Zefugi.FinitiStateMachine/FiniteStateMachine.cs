using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.FinitiStateMachine
{
    public class FiniteStateMachine
    {
        public event FiniteStateMachineEventHandler EnterState;
        protected virtual void OnEnterState() { }

        public event FiniteStateMachineEventHandler ExitState;
        protected virtual void OnExitState() { }

        private List<ExplicitState> _states = new List<ExplicitState>();
        public IReadOnlyList<ExplicitState> States => _states.AsReadOnly();

        public ExplicitState CurrentState { get; internal set; }

        public ExplicitState InitialState { get; internal set; }

        public FiniteStateMachine(ExplicitState initialState)
        {
            initialState.StateMachine = this;
            InitialState = CurrentState = initialState;
            OnEnterState();
            EnterState?.Invoke();
            CurrentState.OnEnter();
        }

        public void Add(ExplicitState state)
        {
            state.StateMachine = this;
            _states.Add(state);
        }

        public void Transition(ExplicitState toState)
        {
            if (CurrentState == toState)
                return;

            if (toState == null)
                throw new ArgumentNullException("toState");
            
            OnExitState();
            ExitState?.Invoke();
            CurrentState.OnExit();

            CurrentState = toState;

            OnEnterState();
            EnterState?.Invoke();
            CurrentState.OnEnter();
        }

        public void Update()
        {
            CurrentState = CurrentState.Update() ?? InitialState;
            CurrentState.Update();
        }
    }
}
