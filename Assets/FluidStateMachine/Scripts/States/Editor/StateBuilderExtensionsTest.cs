using NUnit.Framework;

namespace CleverCrow.FluidStateMachine.Editors {
    public static class StateBuilderExtensions {
        public static StateBuilder MyCustomStateBuilderMethod (this StateBuilder builder) {
            return builder.AddAction(new ActionEnter((action) => { }));
        }
    }

    public class StateBuilderExtensionsTest {
        private enum StateId {
            A,
        }

        [Test]
        public void It_should_call_the_extension_method_from_the_StateBuilder_class () {
            var fsmBuilder = new FsmBuilder()
                .State(StateId.A, (state) => {
                    state
                        .MyCustomStateBuilderMethod()
                        .Update((action) => { });
                });

            fsmBuilder.Build();
        }
    }
}
