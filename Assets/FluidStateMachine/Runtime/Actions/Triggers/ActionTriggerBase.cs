using System;

namespace CleverCrow.Fluid.FSMs {
    public abstract class ActionTriggerBase : ActionBase {
        private readonly string _tag;
        private readonly Action<IAction> _update;
        private bool _triggerUpdate;
        private ITriggerMonitor _monitor;
        
        public ITriggerMonitor Monitor {
            get {
                PopulateMonitor();
                return _monitor;
            }
            set { _monitor = value; }
        }

        protected ActionTriggerBase (string tag, Action<IAction> update) {
            _tag = tag;
            _update = update;
        }

        public void PopulateMonitor () {
            if (_monitor == null) {
                _monitor = ParentState.ParentFsm.Owner.GetComponent<ITriggerMonitor>() 
                          ?? ParentState.ParentFsm.Owner.AddComponent<ActionTriggerMonitor>();
            }
        }
        
        protected override void OnUpdate () {
            if (!_triggerUpdate) return;

            _update.Invoke(this);
            _triggerUpdate = false;
        }

        protected void UpdateTrigger (ICollider collider) {
            if (string.IsNullOrEmpty(_tag)) {
                _triggerUpdate = true;
                return;
            }

            _triggerUpdate = collider.CompareTag(_tag);
        }
    }
}