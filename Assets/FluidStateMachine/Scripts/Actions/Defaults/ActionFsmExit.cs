namespace CleverCrow.FluidStateMachine {
    public class ActionFsmExit : ActionBase {
        protected override void OnEnter () {
            ParentState.ParentFsm.Exit();
        }
    }
}