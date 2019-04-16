namespace CleverCrow.FluidStateMachine {
    public interface IAction {
        string Name { get; }

        void Update ();
        void Enter ();
        void Exit ();
    }
}
