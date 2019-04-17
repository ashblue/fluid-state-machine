using NSubstitute;
using NUnit.Framework;
using UnityEngine;

namespace CleverCrow.FluidStateMachine.Editors {
    public class FsmTest {
        private GameObject _owner;
        private Fsm _fsm;
        private IState _state;

        private enum StateId {
            A,
            B,
        }

        [SetUp]
        public void BeforeEach () {
            _owner = new GameObject();
            _fsm = new Fsm(_owner);
            _state = Substitute.For<IState>();
            _state.Id.Returns(StateId.A);
        }

        [TearDown]
        public void AfterEach () {
            Object.DestroyImmediate(_owner);
        }
        
        public class SetStateMethod : FsmTest {
            [Test]
            public void It_should_set_the_current_state () {
                _fsm.AddState(_state);
                _fsm.SetState(_state.Id);
                
                Assert.AreEqual(_state, _fsm.CurrentState);
            }

            [Test]
            public void It_should_call_enter_on_the_set_state () {
                _fsm.AddState(_state);
                _fsm.SetState(_state.Id);

                _state.Received(1).Enter();
            }
            
            [Test]
            public void It_should_call_exit_on_the_previous_state () {
                var stateAlt = Substitute.For<IState>();
                stateAlt.Id.Returns(StateId.B);

                _fsm.AddState(stateAlt);
                _fsm.SetState(stateAlt.Id);
                
                _fsm.AddState(_state);
                _fsm.SetState(_state.Id);

                stateAlt.Received(1).Exit();
            }
        }

        public class TickMethod : FsmTest {
            [Test]
            public void It_should_call_Update_on_the_current_state () {
                _fsm.AddState(_state);
                _fsm.SetState(_state.Id);

                _fsm.Tick();

                _state.Received(1).Update();
            }
        }
    }
}