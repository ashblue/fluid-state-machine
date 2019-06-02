using System;
using UnityEngine.Events;

namespace CleverCrow.Fluid.FSMs.Editors {
    public class ActionTriggerExitTest {
        public class UpdateMethod : ActionTriggerBaseTest.UpdateMethod {
            protected override UnityEvent<ICollider> GetEventTrigger (ITriggerMonitor monitor) {
                return monitor.EventTriggerExit;
            }

            protected override ActionTriggerBase GetNewActionTrigger (string tag, Action<IAction> action) {
                return new ActionTriggerExit(tag, action);
            }
        }
    }
}