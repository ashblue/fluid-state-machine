using UnityEngine;

namespace CleverCrow.FluidStateMachine.Examples {
    public class CustomStateBuilder : StateBuilderBase<CustomStateBuilder> {
        public CustomStateBuilder MyCustomStateBuilderMethod () {
            _actions.Add(new ActionEnter((action) => {
                Debug.Log("Registered");
            }));
            return this;
        }
    }
    
    public class FsmBuilderExample : FsmBuilderBase<FsmBuilderExample, CustomStateBuilder> {
        public void MyCustomFsmMethod () {
            Debug.Log("Custom method");
        }
    }

    public class FsmBuilderCustomUsage {
        private enum StateId {
            A,
        }
        
        public void Init () {
            var fsmBuilder = new FsmBuilderExample()
                .State(StateId.A, (state) => {
                    state
                        .MyCustomStateBuilderMethod()
                        .Update((action) => { });
                });
            
            fsmBuilder.MyCustomFsmMethod();

            var fsm = fsmBuilder.Build();
            fsm.Tick();
        }
    }
}
