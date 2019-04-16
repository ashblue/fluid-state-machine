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

        public StateBuilder Update (string actionName, Action action) {
            _actions.Add(new ActionUpdate(actionName, action));
            return this;
        }
        
        public StateBuilder Enter (string actionName, Action action) {
            _actions.Add(new ActionEnter(actionName, action));
            return this;
        }
        
        public StateBuilder Exit (string actionName, Action action) {
            _actions.Add(new ActionExit(actionName, action));
            return this;
        }
        
        public IState Build () {
            var state = new State(_id);
            
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
