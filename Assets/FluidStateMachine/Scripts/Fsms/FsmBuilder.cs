using System;
using System.Collections.Generic;
using CleverCrow.FluidStateMachine.Editors;

namespace CleverCrow.FluidStateMachine {
    public class FsmBuilder {
        private List<StateData> _stateData = new List<StateData>();
        
        private class StateData {
            public Enum id;
            public Action<StateBuilder> callback;
        }
        
        public IFsm Build () {
            var fsm = new Fsm() as IFsm;
            foreach (var state in _stateData) {
                var builder = new StateBuilder(state.id);
                state.callback(builder);
                fsm.AddState(builder.Build(fsm));                
            }

            return fsm;
        }

        public FsmBuilder State (Enum id, Action<StateBuilder> stateCallback) {
            var builder = new StateBuilder(id);
            stateCallback(builder);
            _stateData.Add(new StateData {
                id = id,
                callback = stateCallback
            });
            
            return this;
        }
    }
}