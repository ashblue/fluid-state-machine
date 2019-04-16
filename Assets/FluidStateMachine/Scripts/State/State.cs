using System;

namespace CleverCrow.FluidStateMachine {
    public class State : IState {
        public Enum Id { get; }
        
        public State (Enum id) {
            Id = id;
        }
    }
}