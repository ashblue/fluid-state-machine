using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine {
    public interface ITriggerMonitor {
        UnityEvent<ICollider> EventTriggerStay { get; }
    }
}
