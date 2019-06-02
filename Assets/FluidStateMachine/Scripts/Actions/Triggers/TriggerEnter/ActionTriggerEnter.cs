using System;

namespace CleverCrow.Fluid.FSMs {
    public class ActionTriggerEnter : ActionTriggerBase {
        public ActionTriggerEnter (string tag, Action<IAction> update) : base(tag, update) {
        }
        
        protected override void OnInit () {
            Monitor.EventTriggerEnter.AddListener(UpdateTrigger);
        }
    }
}