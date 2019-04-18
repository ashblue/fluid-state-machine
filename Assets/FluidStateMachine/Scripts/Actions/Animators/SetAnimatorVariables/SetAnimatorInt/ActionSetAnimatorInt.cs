namespace CleverCrow.FluidStateMachine {
    public class ActionSetAnimatorInt : ActionSetAnimatorVariableBase {
        private readonly string _paramName;
        private readonly int _value;

        public override string Name { get; set; } = "Set Animator Int";

        public ActionSetAnimatorInt (string paramName, int value) {
            _paramName = paramName;
            _value = value;
        }

        protected override void OnEnter () {
            _animator.SetInteger(_paramName, _value);
        }
    }
}