using System;

namespace CleverCrow.FluidStateMachine {
    public class ActionTriggerStay : ActionBase {
        private bool _triggerUpdate;
        private readonly Action _update;
        private readonly string _tag;
        
        public override string Name { get; set; } = "Trigger Stay";
        public ITriggerMonitor Monitor { get; set; }

        public ActionTriggerStay (string tag, Action update) {
            _tag = tag;
            _update = update;
        }

        protected override void OnInit () {
            if (Monitor == null) {
                Monitor = ParentState.ParentFsm.Owner.GetComponent<ITriggerMonitor>() 
                          ?? ParentState.ParentFsm.Owner.AddComponent<ActionTriggerMonitor>();
            }
            
            Monitor.EventTriggerStay.AddListener((collider) => {
                if (string.IsNullOrEmpty(_tag)) {
                    _triggerUpdate = true;
                    return;
                }

                _triggerUpdate = collider.CompareTag(_tag);
            });
        }

        protected override void OnUpdate () {
            if (!_triggerUpdate) return;

            _update.Invoke();
            _triggerUpdate = false;
        }
    }
}
