using System;

namespace CleverCrow.FluidStateMachine {
    /// <summary>
    /// An action triggered every frame
    /// </summary>
    public class ActionUpdate : ActionBase {
        private readonly Action _update;

        public override string Name { get; set; } = "Update";

        public ActionUpdate (Action update) {
            _update = update;
        }

        protected override void OnUpdate () {
            _update();
        }
    }
}