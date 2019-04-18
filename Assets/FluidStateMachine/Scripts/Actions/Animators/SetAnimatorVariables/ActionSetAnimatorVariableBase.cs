using UnityEngine;

namespace CleverCrow.FluidStateMachine {
    public abstract class ActionSetAnimatorVariableBase : ActionBase {
        protected Animator _animator;

        protected override void OnInit () {
            _animator = ParentState.ParentFsm.Owner.GetComponent<Animator>();
        }
    }
}