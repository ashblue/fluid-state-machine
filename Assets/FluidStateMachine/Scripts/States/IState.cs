using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.FluidStateMachine {
    public interface IState {
        Enum Id { get; }
        List<IAction> Actions { get; }
        GameObject GameObject { get; }

        ITransition GetTransition (string name);
    }
}
