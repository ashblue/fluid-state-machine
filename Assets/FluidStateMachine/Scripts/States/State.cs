using System;
using System.Collections.Generic;
using DefaultNamespace;

namespace CleverCrow.FluidStateMachine {
    public class State : IState {
        private readonly Dictionary<string, ITransition> _transitions = new Dictionary<string, ITransition>();
        
        public Enum Id { get; }
        public List<IAction> Actions { get; } = new List<IAction>();

        public State (Enum id) {
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
    }
}