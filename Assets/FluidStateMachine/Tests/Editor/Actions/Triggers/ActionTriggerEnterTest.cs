using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.FSMs.Editors {
    public class ActionTriggerEnterTest {
        public class UpdateMethod : ActionTriggerBaseTest.UpdateMethod {
            protected override UnityEvent<ICollider> GetEventTrigger (ITriggerMonitor monitor) {
                return monitor.EventTriggerEnter;
            }

            protected override ActionTriggerBase GetNewActionTrigger (string tag, Action<IAction> action) {
                return new ActionTriggerEnter(tag, action);
            }
        }
    }
}