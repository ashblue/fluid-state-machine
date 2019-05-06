using System;
using System.Collections.Generic;

namespace CleverCrow.FluidStateMachine {
    public class StateBuilderBase<T> where T : StateBuilderBase<T> {
        protected readonly List<ITransition> _transitions = new List<ITransition>();
        protected readonly List<IAction> _actions = new List<IAction>();

        public Enum Id { get; set; }

        public T SetTransition (string change, Enum id) {
            _transitions.Add(new Transition(change, id));
            return (T) this;
        }

        public T SetAnimatorTrigger (string name) {
            _actions.Add(new ActionSetAnimatorTrigger(name));
            return (T) this;
        }

        public T SetAnimatorBool (string name, bool value) {
            _actions.Add(new ActionSetAnimatorBool(name, value));
            return (T) this;
        }

        public T SetAnimatorInt (string name, int value) {
            _actions.Add(new ActionSetAnimatorInt(name, value));
            return (T) this;
        }

        public T SetAnimatorFloat (string name, float value) {
            _actions.Add(new ActionSetAnimatorFloat(name, value));
            return (T) this;
        }

        public T Update (Action<IAction> action) {
            _actions.Add(new ActionUpdate(action));
            return (T) this;
        }

        public T Update (string actionName, Action<IAction> action) {
            _actions.Add(new ActionUpdate(action) {
                Name = actionName,
            });
            return (T) this;
        }

        public T Enter (string actionName, Action<IAction> action) {
            _actions.Add(new ActionEnter(action) {
                Name = actionName,
            });
            return (T) this;
        }

        public T Enter (Action<IAction> action) {
            _actions.Add(new ActionEnter(action));
            return (T) this;
        }

        public T Exit (string actionName, Action<IAction> action) {
            _actions.Add(new ActionExit(action) {
                Name = actionName,
            });
            return (T) this;
        }

        public T Exit (Action<IAction> action) {
            _actions.Add(new ActionExit(action));
            return (T) this;
        }

        public T TriggerStay (string tag, Action<IAction> action) {
            _actions.Add(new ActionTriggerStay(tag, action));
            return (T) this;
        }

        public T TriggerStay (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            var triggerStay = new ActionTriggerStay(tag, action) {
                Monitor = monitor
            };

            _actions.Add(triggerStay);

            return (T) this;
        }

        public T TriggerEnter (string tag, Action<IAction> action) {
            _actions.Add(new ActionTriggerEnter(tag, action));
            return (T) this;
        }

        public T TriggerEnter (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            var triggerEnter = new ActionTriggerEnter(tag, action) {
                Monitor = monitor
            };

            _actions.Add(triggerEnter);

            return (T) this;
        }

        public T TriggerExit (string tag, Action<IAction> action) {
            _actions.Add(new ActionTriggerExit(tag, action));
            return (T) this;
        }

        public T TriggerExit (ITriggerMonitor monitor, string tag, Action<IAction> action) {
            var triggerExit = new ActionTriggerExit(tag, action) {
                Monitor = monitor
            };

            _actions.Add(triggerExit);

            return (T) this;
        }

        public T RunFsm (string exitTransition, IFsm fsm) {
            _actions.Add(new ActionRunFsm(exitTransition, fsm));
            return (T) this;
        }

        public T FsmExit () {
            _actions.Add(new ActionFsmExit());
            return (T) this;
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