using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine {
    public interface ITriggerMonitor {
        UnityEvent<ICollider> EventTriggerStay { get; }
        UnityEvent<ICollider> EventTriggerEnter { get; }
        UnityEvent<ICollider> EventTriggerExit { get; }
    }
}
