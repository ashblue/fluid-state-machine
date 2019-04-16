using System;
using System.Collections.Generic;
using CleverCrow.FluidStateMachine.Editors;

namespace CleverCrow.FluidStateMachine {
    public class FsmBuilder {
        private List<IState> _states = new List<IState>();
        
        public IFsm Build () {
            var fsm = new Fsm() as IFsm;
            foreach (var state in _states) {
                fsm.AddState(state);                
            }

            return fsm;
        }

        public FsmBuilder State (Enum id, Action<StateBuilder> stateCallback) {
            var builder = new StateBuilder(id);
            stateCallback(builder);
            _states.Add(builder.Build());
            
            return this;
        }
    }
}