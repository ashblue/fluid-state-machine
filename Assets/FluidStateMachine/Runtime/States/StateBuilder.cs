using System;
using System.Collections.Generic;

namespace CleverCrow.Fluid.FSMs {
    public class StateBuilder {
        private readonly List<ITransition> _transitions = new List<ITransition>();
        private readonly List<IAction> _actions = new List<IAction>();

        public Enum Id { get; set; }

        public StateBuilder SetTransition (string change, Enum id) {
            _transitions.Add(new Transition(change, id));
            return this;
        }

        public StateBuilder SetAnimatorTrigger (string name) {
            return AddAction(new ActionSetAnimatorTrigger(name));
        }

        public StateBuilder SetAnimatorBool (string name, bool value) {
            return AddAction(new ActionSetAnimatorBool(name, value));
        }

        public StateBuilder SetAnimatorInt (string name, int value) {
            return AddAction(new ActionSetAnimatorInt(name, value));
        }

        public StateBuilder SetAnimatorFloat (string name, float value) {
            return AddAction(new ActionSetAnimatorFloat(name, value));
        }

        public StateBuilder Update (Action<IAction> action) {
            return AddAction(new ActionUpdate(action));
        }

        public StateBuilder Update (string actionName, Action<IAction> action) {
            return AddAction(new ActionUpdate(action) {
                Name = actionName,
            });
        }

        public StateBuilder Enter (string actionName, Action<IAction> action) {
            return AddAction(new ActionEnter(action) {
                Name = actionName,
            });
        }

        public StateBuilder Enter (Action<IAction> action) {
            return AddAction(new ActionEnter(action));
        }

        public StateBuilder Exit (string actionName, Action<IAction> action) {
            return AddAction(new ActionExit(action) {
                Name = actionName,
            });
        }

        public StateBuilder Exit (Action<IAction> action) {
            return AddAction(new ActionExit(action));
        }

        public StateBuilder TriggerStay (string tag, Action<IAction> action) {
            return AddAction(new ActionTriggerStay(tag, action));
        }

        public StateBuilder TriggerStay (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            return AddAction(new ActionTriggerStay(tag, action) {
                Monitor = monitor
            });
        }

        public StateBuilder TriggerEnter (string tag, Action<IAction> action) {
            return AddAction(new ActionTriggerEnter(tag, action));
        }

        public StateBuilder TriggerEnter (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            return AddAction(new ActionTriggerEnter(tag, action) {
                Monitor = monitor
            });
        }

        public StateBuilder TriggerExit (string tag, Action<IAction> action) {
            return AddAction(new ActionTriggerExit(tag, action));
        }

        public StateBuilder TriggerExit (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            return AddAction(new ActionTriggerExit(tag, action) {
                Monitor = monitor
            });
        }

        public StateBuilder RunFsm (string exitTransition, IFsm fsm) {
            return AddAction(new ActionRunFsm(exitTransition, fsm));
        }

        public StateBuilder FsmExit () {
            return AddAction(new ActionFsmExit());
        }
        
        public StateBuilder AddAction (IAction action) {
            _actions.Add(action);
            return this;
        }

        public IState Build (IFsm fsm) {
            var state = new State(fsm, Id);

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
