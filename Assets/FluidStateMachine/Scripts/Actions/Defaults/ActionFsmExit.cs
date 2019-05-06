namespace CleverCrow.FluidStateMachine {
    /// <summary>
    /// Triggers the Exit method on the current parent FSM
    /// </summary>
    public class ActionFsmExit : ActionBase {
        protected override void OnEnter () {
            ParentState.ParentFsm.Exit();
        }
    }
}