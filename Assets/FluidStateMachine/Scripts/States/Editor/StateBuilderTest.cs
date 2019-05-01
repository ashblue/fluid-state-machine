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

        public class DefaultActions {
            public class UpdateMethod : StateBuilderTest {
                [Test]
                public void It_should_add_an_UpdateAction () {
                    var state = _builder
                        .Update(() => { })
                        .Build(_fsm);

                    Assert.IsTrue(state.Actions[0] is ActionUpdate);
                }

                [Test]
                public void It_should_add_an_UpdateAction_with_the_expected_Action_name () {
                    var state = _builder
                        .Update("custom action", () => { })
                        .Build(_fsm);

                    Assert.AreEqual("custom action", state.Actions[0].Name);
                }

                [Test]
                public void It_should_attach_the_state_to_actions () {
                    var state = _builder
                        .Update(() => { })
                        .Build(_fsm);

                    Assert.AreEqual(state, state.Actions[0].ParentState);
                }
            }

            public class EnterMethod : StateBuilderTest {
                [Test]
                public void It_should_add_an_ActionEnter () {
                    var state = _builder
                        .Enter(() => { })
                        .Build(_fsm);

                    Assert.IsTrue(state.Actions[0] is ActionEnter);
                }

                [Test]
                public void It_should_add_an_ActionEnter_with_the_expected_name () {
                    var state = _builder
                        .Enter("custom action", () => { })
                        .Build(_fsm);

                    Assert.AreEqual("custom action", state.Actions[0].Name);
                }
            }

            public class ExitMethod : StateBuilderTest {
                [Test]
                public void It_should_add_an_ActionExit () {
                    var state = _builder
                        .Exit(() => { })
                        .Build(_fsm);

                    Assert.IsTrue(state.Actions[0] is ActionExit);
                }

                [Test]
                public void It_should_add_an_ActionExit_with_the_expected_name () {
                    var state = _builder
                        .Exit("custom action", () => { })
                        .Build(_fsm);

                    Assert.AreEqual("custom action", state.Actions[0].Name);
                }
            }
        }

        public class AnimatorActions {
            public class SetAnimatorTriggerMethod : StateBuilderTest {
                [Test]
                public void It_should_add_an_ActionSetAnimatorTrigger () {
                    var state = _builder
                        .SetAnimatorTrigger("name")
                        .Build(_fsm);

                    Assert.IsTrue(state.Actions[0] is ActionSetAnimatorTrigger);
                }
            }
            
            public class SetAnimatorBoolMethod : StateBuilderTest {
                [Test]
                public void It_should_add_an_ActionSetAnimatorBool () {
                    var state = _builder
                        .SetAnimatorBool("name", true)
                        .Build(_fsm);

                    Assert.IsTrue(state.Actions[0] is ActionSetAnimatorBool);
                }
            }
            
            public class SetAnimatorIntMethod : StateBuilderTest {
                [Test]
                public void It_should_add_an_ActionSetAnimatorInt () {
                    var state = _builder
                        .SetAnimatorInt("name", 1)
                        .Build(_fsm);

                    Assert.IsTrue(state.Actions[0] is ActionSetAnimatorInt);
                }
            }
            
            public class SetAnimatorFloatMethod : StateBuilderTest {
                [Test]
                public void It_should_add_an_ActionSetAnimatorFloat () {
                    var state = _builder
                        .SetAnimatorFloat("name", 1)
                        .Build(_fsm);

                    Assert.IsTrue(state.Actions[0] is ActionSetAnimatorFloat);
                }
            }
        }

        public class TriggerActions {
            public class TriggerStay : StateBuilderTest {
                [Test]
                public void It_should_create_an_ActionTriggerStay () {
                    var state = _builder
                        .TriggerStay("Player", () => {})
                        .Build(_fsm);
                    
                    Assert.IsTrue(state.Actions[0] is ActionTriggerStay);
                }

                [Test]
                public void It_should_allow_overriding_the_trigger_monitor () {
                    var monitor = Substitute.For<ITriggerMonitor>();
                    var state = _builder
                        .TriggerStay(monitor, "Player", () => {})
                        .Build(_fsm);

                    var trigger = state.Actions[0] as ActionTriggerStay;
                    
                    Assert.AreEqual(trigger.Monitor, monitor);
                }
            }
        }
    }
}