namespace CleverCrow.Fluid.FSMs {
    public class ActionSetAnimatorFloat : ActionSetAnimatorVariableBase {
        private readonly string _paramName;
        private readonly float _value;

        public override string Name { get; set; } = "Set Animator Float";

        public ActionSetAnimatorFloat (string paramName, float value) {
            _paramName = paramName;
            _value = value;
        }

        protected override void OnEnter () {
            _animator.SetFloat(_paramName, _value);
        }
    }
}