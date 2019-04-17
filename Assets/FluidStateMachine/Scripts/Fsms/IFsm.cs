using System;

namespace CleverCrow.FluidStateMachine {
    public interface IFsm {
        IState GetState (Enum id);
        void AddState (IState state);
        void SetState (Enum id);
    }
}