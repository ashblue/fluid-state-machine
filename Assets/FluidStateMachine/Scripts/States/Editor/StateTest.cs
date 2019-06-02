using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace CleverCrow.Fluid.FSMs.Editors {
    public class StateTest {
        private IFsm _fsm;
        private State _state;

        private enum StateId {
            A,
            B,
        }

        [SetUp]
        public void BeforeEach () {
            _fsm = Substitute.For<IFsm>();
            _state = new State(_fsm, StateId.A);
        }

        public class GameObjectProperty : StateTest {
            [Test]
            public void It_should_reference_the_fsm_GameObject () {
                var go = new GameObject();
                _fsm.Owner.Returns(go);
                
                Assert.AreEqual(go, _state.ParentFsm.Owner);
                
                Object.DestroyImmediate(go);
            }
        }
        
        public class UpdateMethod : StateTest {
            [Test]
            public void It_should_call_Update_on_all_Actions () {
                var action = Substitute.For<IAction>();
                var state = new State(_fsm, StateId.A);
                state.Actions.Add(action);

                state.Update();
                
                action.Received(1).Update();
            }
        }
        
        public class EnterMethod : StateTest {
            [Test]
            public void It_should_call_Enter_on_all_Actions () {
                var action = Substitute.For<IAction>();
                var state = new State(_fsm, StateId.A);
                state.Actions.Add(action);

                state.Enter();
                
                action.Received(1).Enter();
            }
        }

        public class ExitMethod : StateTest {
            [Test]
            public void It_should_call_Exit_on_all_Actions () {
                var action = Substitute.For<IAction>();
                var state = new State(_fsm, StateId.A);
                state.Actions.Add(action);

                state.Exit();
                
                action.Received(1).Exit();
            }
        }

        public class TransitionMethod : StateTest {
            [Test]
            public void It_should_activate_SetState_on_the_fsm () {
                var state = new State(_fsm, StateId.A);
                
                state.AddTransition(new Transition("b", StateId.B));
                state.Transition("b");

                _fsm.Received(1).SetState(StateId.B);
            }

            [Test]
            public void It_should_silently_fail_if_no_transition_exists () {
                var state = new State(_fsm, StateId.A);
                
                state.Transition("b");
            }
        }
    }
}