using System;
using System.Collections.Generic;

namespace CleverCrow.FluidStateMachine {
    public class Fsm : IFsm {
        private readonly Dictionary<Enum, IState> _stateDic = new Dictionary<Enum, IState>();
        
        public IState GetState (Enum id) {
            return _stateDic[id];
        }

        public void AddState (IState state) {
            _stateDic[state.Id] = state;
        }

        public void SetState (Enum id) {
            throw new NotImplementedException();
        }
    }
}