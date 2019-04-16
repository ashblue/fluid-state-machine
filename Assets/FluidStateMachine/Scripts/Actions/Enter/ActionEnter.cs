using System;

namespace CleverCrow.FluidStateMachine {
    /// <summary>
    /// An action triggered when the state is entered
    /// </summary>
    public class ActionEnter : ActionBase {
        private readonly Action _enter;

        public ActionEnter (string name, Action enter) {
            Name = name;
            _enter = enter;
        }

        protected override void OnEnter () {
            _enter();
        }
    }
}