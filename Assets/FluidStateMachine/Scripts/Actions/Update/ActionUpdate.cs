using System;

namespace CleverCrow.FluidStateMachine {
    /// <summary>
    /// An action triggered every frame
    /// </summary>
    public class ActionUpdate : ActionBase {
        private readonly Action _update;

        public ActionUpdate (string name, Action update) {
            Name = name;
            _update = update;
        }

        protected override void OnUpdate () {
            _update();
        }
    }
}