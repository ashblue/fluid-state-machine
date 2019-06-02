using System;

namespace CleverCrow.Fluid.FSMs {
    public class ActionTriggerStay : ActionTriggerBase {
        public ActionTriggerStay (string tag, Action<IAction> update) : base(tag, update) {
        }
        
        protected override void OnInit () {
            Monitor.EventTriggerStay.AddListener(UpdateTrigger);
        }
    }
}
