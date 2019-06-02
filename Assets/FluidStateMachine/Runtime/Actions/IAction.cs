namespace CleverCrow.Fluid.FSMs {
    public interface IAction {
        string Name { get; }
        IState ParentState { get; set; }

        void Update ();
        void Enter ();
        void Exit ();
        void Transition (string id);
    }
}
