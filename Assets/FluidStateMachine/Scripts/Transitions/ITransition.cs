using System;

namespace CleverCrow.FluidStateMachine {
    public interface ITransition {
        string Name { get; }
        Enum Target { get; }
    }
}