namespace CleverCrow.Fluid.FSMs {
    public abstract class ActionBase : IAction {
        private bool _init;
        
        public virtual string Name { get; set; } = "Untitled";
        public IState ParentState { get; set; }

        public void Update () {
            OnUpdate();
        }
        
        /// <summary>
        /// Triggered every FSM tick when this state is active
        /// </summary>
        protected virtual void OnUpdate () {}

        public void Enter () {
            Init();
            OnEnter();
        }
        
        /// <summary>
        /// Triggered when entering the state
        /// </summary>
        protected virtual void OnEnter () {}

        /// <summary>
        /// Triggered when exiting the state
        /// </summary>
        public void Exit () {
            OnExit();
        }

        public void Transition (string id) {
            ParentState.Transition(id);
        }

        protected virtual void OnExit () {}

        /// <summary>
        /// Triggered the first time this FSM runs Enter
        /// </summary>
        private void Init () {
            if (_init) return;
            
            OnInit();
            _init = true;
        }
        
        protected virtual void OnInit () {}
    }
}