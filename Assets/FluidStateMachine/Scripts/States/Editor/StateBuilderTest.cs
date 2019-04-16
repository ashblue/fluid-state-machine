using NUnit.Framework;

namespace CleverCrow.FluidStateMachine.Editors {
    public class StateBuilderTest {
        private enum StateEnum {
            A,
            B,
        }
        
        public class BuildMethod {
            [Test]
            public void It_should_create_a_State () {
                var builder = new StateBuilder(StateEnum.A);
                
                Assert.IsTrue(builder.Build() is State);
            }
        }

        public class TransitionMethod {
            [Test]
            public void It_should_add_a_transition_to_another_state () {
                var builder = new StateBuilder(StateEnum.A);
                var state = builder
                    .Transition("change", StateEnum.B)
                    .Build();
                var transition = state.GetTransition("change");
                
                Assert.AreEqual(StateEnum.B, transition.Target);
            }
        }

        public class UpdateMethod {
            [Test]
            public void It_should_add_an_UpdateAction_with_the_expected_Action_name () {
                var builder = new StateBuilder(StateEnum.A);
                var state = builder
                    .Update("custom action", () => { })
                    .Build();
                
                Assert.AreEqual("custom action", state.Actions[0].Name);
            }
        }
    }
}