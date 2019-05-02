using System;

namespace CleverCrow.FluidStateMachine {
    public class ActionTriggerStay : ActionTriggerBase {
        public ActionTriggerStay (string tag, Action update) : base(tag, update) {
        }
        
        protected override void OnInit () {
            Monitor.EventTriggerStay.AddListener(UpdateTrigger);
        }
    }
}
