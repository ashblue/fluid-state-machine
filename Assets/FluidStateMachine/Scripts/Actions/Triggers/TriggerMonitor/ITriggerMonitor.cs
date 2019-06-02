using UnityEngine.Events;

namespace CleverCrow.Fluid.FSMs {
    public interface ITriggerMonitor {
        UnityEvent<ICollider> EventTriggerStay { get; }
        UnityEvent<ICollider> EventTriggerEnter { get; }
        UnityEvent<ICollider> EventTriggerExit { get; }
    }
}
