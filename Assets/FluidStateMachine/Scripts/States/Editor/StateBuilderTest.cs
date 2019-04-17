using NSubstitute;
using NUnit.Framework;

namespace CleverCrow.FluidStateMachine.Editors {
    public class StateBuilderTest {
        private StateBuilder _builder;
        private IFsm _fsm;
        
        private enum StateEnum {
            A,
            B,
        }

        [SetUp]
        public void BeforeEach () {
            _fsm = Substitute.For<IFsm>();
            _builder = new StateBuilder(StateEnum.A);
        }
        
        public class BuildMethod : StateBuilderTest {
            [Test]
            public void It_should_create_a_State () {
                Assert.IsTrue(_builder.Build(_fsm) is State);
            }
        }

        public class TransitionMethod : StateBuilderTest {
            [Test]
            public void It_should_add_a_transition_to_another_state () {
                var state = _builder
                    .Transition("change", StateEnum.B)
                    .Build(_fsm);
                var transition = state.GetTransition("change");
                
                Assert.AreEqual(StateEnum.B, transition.Target);
            }
        }

        public class UpdateMethod : StateBuilderTest {
            [Test]
            public void It_should_add_an_UpdateAction_with_the_expected_Action_name () {
                var state = _builder
                    .Update("custom action", () => { })
                    .Build(_fsm);
                
                Assert.AreEqual("custom action", state.Actions[0].Name);
            }
        }

        public class EnterMethod : StateBuilderTest {
            [Test]
            public void It_should_add_an_ActionEnter () {
                var state = _builder
                    .Enter("custom action", () => { })
                    .Build(_fsm);
                
                Assert.IsTrue(state.Actions[0] is ActionEnter);
            }
        }
        
        public class ExitMethod : StateBuilderTest {
            [Test]
            public void It_should_add_an_ActionExit () {
                var state = _builder
                    .Exit("custom action", () => { })
                    .Build(_fsm);
                
                Assert.IsTrue(state.Actions[0] is ActionExit);
            }
        }
    }
}