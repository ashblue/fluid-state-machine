using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.Fluid.FSMs {
    public class ActionTriggerMonitor : MonoBehaviour, ITriggerMonitor {
        private class EventCollider : UnityEvent<ICollider> {}

        public UnityEvent<ICollider> EventTriggerStay { get; } = new EventCollider();
        public UnityEvent<ICollider> EventTriggerEnter { get; } = new EventCollider();
        public UnityEvent<ICollider> EventTriggerExit { get; } = new EventCollider();

        private class ColliderSimple : ICollider {
            private readonly Collider _collider;
            
            public ColliderSimple (Collider collider) {
                _collider = collider;
            }
            
            public bool CompareTag (string tag) {
                return _collider.CompareTag(tag);
            }
        }

        private void OnTriggerStay (Collider other) {
            EventTriggerStay.Invoke(new ColliderSimple(other));
        }

        private void OnTriggerEnter (Collider other) {
            EventTriggerEnter.Invoke(new ColliderSimple(other));
        }

        private void OnTriggerExit (Collider other) {
            EventTriggerExit.Invoke(new ColliderSimple(other));
        }
    }
}
