using System;

namespace CleverCrow.FluidStateMachine {
    public class ActionTriggerStay : ActionBase {
        private bool _triggerUpdate;
        private readonly Action _update;
        
        public override string Name { get; set; } = "Trigger Stay";

        public ActionTriggerStay (ITriggerMonitor monitor, string match, Action update) {
            _update = update;
            monitor.EventTriggerStay.AddListener((collider) => {
                if (string.IsNullOrEmpty(match)) {
                    _triggerUpdate = true;
                    return;
                }

                _triggerUpdate = collider.CompareTag(match);
            });
        }

        protected override void OnUpdate () {
            if (!_triggerUpdate) return;

            _update.Invoke();
            _triggerUpdate = false;
        }
    }
}
