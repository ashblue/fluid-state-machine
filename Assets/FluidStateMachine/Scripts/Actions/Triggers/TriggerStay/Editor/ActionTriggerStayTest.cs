using System;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionTriggerStayTest {
        public class UpdateMethod : ActionTriggerBaseTest.UpdateMethod {
            protected override UnityEvent<ICollider> GetEventTrigger (ITriggerMonitor monitor) {
                return monitor.EventTriggerStay;
            }

            protected override ActionTriggerBase GetNewActionTrigger (string tag, Action action) {
                return new ActionTriggerStay(tag, action);
            }
        }
    }
}
