namespace CleverCrow.FluidStateMachine {
    public abstract class ActionBase : IAction {
        public string Name { get; protected set; }
        
        public void Update () {
            OnUpdate();
        }
        
        protected virtual void OnUpdate () {}

        public void Enter () {
            OnEnter();
        }
        
        protected virtual void OnEnter () {}

        public void Exit () {
            OnExit();
        }
        
        protected virtual void OnExit () {}
    }
}