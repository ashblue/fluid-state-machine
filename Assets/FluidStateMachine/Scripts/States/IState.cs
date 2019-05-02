using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.FluidStateMachine {
    public interface IState {
        Enum Id { get; }
        List<IAction> Actions { get; }
        IFsm ParentFsm { get; }

        ITransition GetTransition (string name);
        void Enter ();
        void Exit ();
        void Update ();
        void Transition (string id);
    }
}
