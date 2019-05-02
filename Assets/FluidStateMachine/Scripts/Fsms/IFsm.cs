using System;
using UnityEngine;

namespace CleverCrow.FluidStateMachine {
    public interface IFsm {
        IState GetState (Enum id);
        void AddState (IState state);
        void SetState (Enum id);
        GameObject Owner { get; }
        IState CurrentState { get; }
        void Tick ();
    }
}