namespace CleverCrow.Fluid.FSMs {
    public class ActionSetAnimatorBool : ActionSetAnimatorVariableBase {
        private readonly string _paramName;
        private readonly bool _value;

        public override string Name { get; set; } = "Set Animator Bool";

        public ActionSetAnimatorBool (string paramName, bool value) {
            _paramName = paramName;
            _value = value;
        }

        protected override void OnEnter () {
            _animator.SetBool(_paramName, _value);
        }
    }
}