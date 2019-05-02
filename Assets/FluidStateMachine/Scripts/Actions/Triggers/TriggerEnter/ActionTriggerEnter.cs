using System;

namespace CleverCrow.FluidStateMachine {
    public class ActionTriggerEnter : ActionTriggerBase {
        public ActionTriggerEnter (string tag, Action update) : base(tag, update) {
        }
        
        protected override void OnInit () {
            Monitor.EventTriggerEnter.AddListener(UpdateTrigger);
        }
    }
}