using System;

namespace CleverCrow.FluidStateMachine {
    public class ActionExit : ActionBase {
        private readonly Action _exit;

        public ActionExit (string name, Action exit) {
            Name = name;
            _exit = exit;
        }

        protected override void OnExit () {
            _exit();
        }
    }
}