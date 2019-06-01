using UnityEngine;

// @TODO Move these into a state and fsm extension test
namespace CleverCrow.FluidStateMachine.Examples {
    public static class StateBuilderExtensions {
        public static StateBuilder MyCustomStateBuilderMethod (this StateBuilder builder) {
            return builder.AddAction(new ActionEnter((action) => {
                Debug.Log("Registered");
            }));
        }
    }

    public static class FsmBuilderExtensions {
        public static FsmBuilder MyCustomFsmMethod (this FsmBuilder builder) {
            Debug.Log("Custom method");
            return builder;
        }
    }

    public class FsmBuilderCustomUsage {
        private enum StateId {
            A,
        }
        
        public void Init () {
            var fsmBuilder = new FsmBuilder()
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
