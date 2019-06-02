namespace CleverCrow.Fluid.FSMs {
    /// <summary>
    /// Run a nested FSM. Triggers exit transition when nested FSM triggers an Exit event.
    /// </summary>
    public class ActionRunFsm : ActionBase {
        private IFsm _fsm;
        private string _exitTransition;
        private bool _triggerExit;
        
        public override string Name => "Run FSM";

        public ActionRunFsm (string exitTransition, IFsm fsm) {
            _fsm = fsm;
            _exitTransition = exitTransition;
        }
        
        protected override void OnInit () {
            _fsm.EventExit.AddListener(() => _triggerExit = true);
        }

        protected override void OnEnter () {
            _fsm.Reset();
            _triggerExit = false;
        }

        protected override void OnUpdate () {
            if (_triggerExit) {
                Transition(_exitTransition);
                return;
            }
            
            _fsm.Tick();
        }
    }
}