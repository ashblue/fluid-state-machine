using System;

namespace CleverCrow.FluidStateMachine {
    /// <summary>
    /// An action triggered when the state is entered
    /// </summary>
    public class ActionEnter : ActionBase {
        private readonly Action<IAction> _enter;

        public override string Name { get; set; } = "Enter";

        public ActionEnter (Action<IAction> enter) {
            _enter = enter;
        }

        protected override void OnEnter () {
            _enter(this);
        }
    }
}