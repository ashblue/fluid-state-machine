using System;
using System.Collections.Generic;

namespace CleverCrow.FluidStateMachine {
    public class StateBuilder {
        private readonly Enum _id;
        private readonly List<ITransition> _transitions = new List<ITransition>();
        private readonly List<IAction> _actions = new List<IAction>();

        public StateBuilder (Enum id) {
            _id = id;
        }
        
        public StateBuilder SetTransition (string change, Enum id) {
            _transitions.Add(new Transition(change, id));
            return this;
        }
        
        public StateBuilder SetAnimatorTrigger (string name) {
            _actions.Add(new ActionSetAnimatorTrigger(name));
            return this;
        }
        
        public StateBuilder SetAnimatorBool (string name, bool value) {
            _actions.Add(new ActionSetAnimatorBool(name, value));
            return this;
        }
        
        public StateBuilder SetAnimatorInt (string name, int value) {
            _actions.Add(new ActionSetAnimatorInt(name, value));
            return this;
        }
        
        public StateBuilder SetAnimatorFloat (string name, float value) {
            _actions.Add(new ActionSetAnimatorFloat(name, value));
            return this;
        }
        
        public StateBuilder Update (Action<IAction> action) {
            _actions.Add(new ActionUpdate(action));
            return this;
        }

        public StateBuilder Update (string actionName, Action<IAction> action) {
            _actions.Add(new ActionUpdate(action) {
                Name = actionName,
            });
            return this;
        }
        
        public StateBuilder Enter (string actionName, Action<IAction> action) {
            _actions.Add(new ActionEnter(action) {
                Name = actionName,
            });
            return this;
        }
        
        public StateBuilder Enter (Action<IAction> action) {
            _actions.Add(new ActionEnter(action));
            return this;
        }
        
        public StateBuilder Exit (string actionName, Action<IAction> action) {
            _actions.Add(new ActionExit(action) {
                Name = actionName,
            });
            return this;
        }
        
        public StateBuilder Exit (Action<IAction> action) {
            _actions.Add(new ActionExit(action));
            return this;
        }

        public StateBuilder TriggerStay (string tag, Action<IAction> action) {
            _actions.Add(new ActionTriggerStay(tag, action));
            return this;
        }

        public StateBuilder TriggerStay (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            var triggerStay = new ActionTriggerStay(tag, action) {
                Monitor = monitor
            };
            
            _actions.Add(triggerStay);
            
            return this;
        }
        
        public StateBuilder TriggerEnter (string tag, Action<IAction> action) {
            _actions.Add(new ActionTriggerEnter(tag, action));
            return this;
        }
        
        public StateBuilder TriggerEnter (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            var triggerEnter = new ActionTriggerEnter(tag, action) {
                Monitor = monitor
            };
            
            _actions.Add(triggerEnter);
            
            return this;
        }
        
        public StateBuilder TriggerExit (string tag, Action<IAction> action) {
            _actions.Add(new ActionTriggerExit(tag, action));
            return this;
        }
        
        public StateBuilder TriggerExit (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            var triggerExit = new ActionTriggerExit(tag, action) {
                Monitor = monitor
            };
            
            _actions.Add(triggerExit);
            
            return this;
        }
        
        public IState Build (IFsm fsm) {
            var state = new State(fsm, _id);
            
            foreach (var transition in _transitions) {
                state.AddTransition(transition);
            }
            
            foreach (var action in _actions) {
                action.ParentState = state;
                state.Actions.Add(action);
            }
                
            return state;
        }
    }
}
