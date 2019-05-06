using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.FluidStateMachine {
    public class FsmBuilderBase<F, S> 
        where F : FsmBuilderBase<F, S> 
        where S : StateBuilderBase<S>, new() {
        protected readonly List<StateData> _stateData = new List<StateData>();
        protected GameObject _owner;
        protected Enum _defaultState;
        
        protected class StateData {
            public Enum id;
            public Action<S> callback;
        }

        public F Owner (GameObject owner) {
            _owner = owner;
            return (F)this;
        }
        
        public F State (Enum id, Action<S> stateCallback) {
            var builder = new S {Id = id};
            stateCallback(builder);
            _stateData.Add(new StateData {
                id = id,
                callback = stateCallback
            });
            
            return (F)this;
        }
        
        public F Default (Enum id) {
            _defaultState = id;
            return (F)this;
        }
        
        public IFsm Build () {
            var fsm = new Fsm(_owner) as IFsm;
            StateData defaultState = null;
            
            foreach (var state in _stateData) {
                var builder = new S {Id = state.id};
                state.callback(builder);
                fsm.AddState(builder.Build(fsm));

                if (Equals(_defaultState, state.id)) {
                    defaultState = state;
                }
            }

            if (defaultState != null) {
                fsm.DefaultState = fsm.GetState(defaultState.id);
            }

            return fsm;
        }
    }
}