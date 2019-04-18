using UnityEngine;

namespace CleverCrow.FluidStateMachine {
    public class ActionSetAnimatorBool : ActionBase {
        private readonly string _paramName;
        private readonly bool _value;

        public override string Name { get; set; } = "Set Animator Bool";

        public ActionSetAnimatorBool (string paramName, bool value) {
            _paramName = paramName;
            _value = value;
        }

        protected override void OnEnter () {
            var animator = ParentState.ParentFsm.Owner.GetComponent<Animator>();
            animator.SetBool(_paramName, _value);
        }
    }
}