using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine {
    public class ActionTriggerMonitor : MonoBehaviour, ITriggerMonitor {
        private class EventCollider : UnityEvent<ICollider> {}

        public UnityEvent<ICollider> EventTriggerStay { get; } = new EventCollider();
        
        private void OnTriggerStay (Collider other) {
            EventTriggerStay.Invoke(other as ColliderWrapper);
        }
    }
}
