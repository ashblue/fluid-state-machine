using System;
using System.Collections.Generic;
using DefaultNamespace;

namespace CleverCrow.FluidStateMachine {
    public interface IState {
        Enum Id { get; }
        List<IAction> Actions { get; }
        ITransition GetTransition (string name);
    }
}
