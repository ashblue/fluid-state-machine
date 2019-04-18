using System;
using System.Collections.Generic;

namespace CleverCrow.FluidStateMachine.Editors {
    public class StateBuilder {
        private readonly Enum _id;
        private readonly List<ITransition> _transitions = new List<ITransition>();
        private readonly List<IAction> _actions = new List<IAction>();

        public StateBuilder (Enum id) {
            _id = id;
        }
        
        public StateBuilder Transition (string change, Enum id) {
            _transitions.Add(new Transition(change, id));
            return this;
        }
        
        public StateBuilder Update (Action action) {
            _actions.Add(new ActionUpdate(action));
            return this;
        }

        public StateBuilder Update (string actionName, Action action) {
            _actions.Add(new ActionUpdate(action) {
                Name = actionName,
            });
            return this;
        }
        
        public StateBuilder Enter (string actionName, Action action) {
            _actions.Add(new ActionEnter(action) {
                Name = actionName,
            });
            return this;
        }
        
        public StateBuilder Enter (Action action) {
            _actions.Add(new ActionEnter(action));
            return this;
        }
        
        public StateBuilder Exit (string actionName, Action action) {
            _actions.Add(new ActionExit(action) {
                Name = actionName,
            });
            return this;
        }
        
        public StateBuilder Exit (Action action) {
            _actions.Add(new ActionExit(action));
            return this;
        }
        
        public IState Build (IFsm fsm) {
            var state = new State(fsm, _id);
            
            foreach (var transition in _transitions) {
                state.AddTransition(transition);
            }
            
            foreach (var action in _actions) {
                state.Actions.Add(action);
            }
                
            return state;
        }
    }
}
