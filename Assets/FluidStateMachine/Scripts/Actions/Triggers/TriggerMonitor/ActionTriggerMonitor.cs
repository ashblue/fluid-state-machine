using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine {
    public class ActionTriggerMonitor : MonoBehaviour, ITriggerMonitor {
        private class EventCollider : UnityEvent<ICollider> {}

        public UnityEvent<ICollider> EventTriggerStay { get; } = new EventCollider();
        public UnityEvent<ICollider> EventTriggerEnter { get; } = new EventCollider();
        public UnityEvent<ICollider> EventTriggerExit { get; } = new EventCollider();

        private void OnTriggerStay (Collider other) {
            EventTriggerStay.Invoke(other as ColliderWrapper);
        }

        private void OnTriggerEnter (Collider other) {
            EventTriggerEnter.Invoke(other as ColliderWrapper);
        }

        private void OnTriggerExit (Collider other) {
            EventTriggerExit.Invoke(other as ColliderWrapper);
        }
    }
}
