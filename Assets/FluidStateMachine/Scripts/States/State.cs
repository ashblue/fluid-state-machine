using System;
using System.Collections.Generic;

namespace CleverCrow.FluidStateMachine {
    public class State : IState {
        private IFsm _fsm;
        private readonly Dictionary<string, ITransition> _transitions = new Dictionary<string, ITransition>();
        
        public Enum Id { get; }
        public List<IAction> Actions { get; } = new List<IAction>();

        public State (IFsm fsm, Enum id) {
            _fsm = fsm;
            Id = id;
        }

        public void AddTransition (ITransition transition) {
            _transitions[transition.Name] = transition;
        }
        
        public ITransition GetTransition (string name) {
            return _transitions[name];
        }
        
        public void Update () {
            foreach (var action in Actions) {
                action.Update();
            }
        }

        public void Enter () {
            foreach (var action in Actions) {
                action.Enter();
            }
        }

        public void Exit () {
            foreach (var action in Actions) {
                action.Exit();
            }
        }

        public void Transition (string id) {
            var transition = GetTransition(id);
            _fsm.SetState(transition.Target);
        }
    }
}