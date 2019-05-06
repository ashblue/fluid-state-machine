using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionRunFsmEditor {
        private IFsm _fsm;
        
        [SetUp]
        public void BeforeEach () {
            var unityEvent = new UnityEvent();
            _fsm = Substitute.For<IFsm>();
            _fsm.EventExit.Returns(unityEvent);
        }
        
        public class EnterMethod : ActionRunFsmEditor {
            [Test]
            public void It_should_trigger_Reset_on_the_fsm () {
                var runFsm = new ActionRunFsm(_fsm, "a");

                runFsm.Enter();

                _fsm.Received(1).Reset();
            }
        }
        
        public class UpdateMethod : ActionRunFsmEditor {
            [Test]
            public void It_should_tick_the_nested_fsm () {
                var runFsm = new ActionRunFsm(_fsm, "a");
                
                runFsm.Update();
                
                _fsm.Received(1).Tick();
            }

            [Test]
            public void It_should_trigger_an_Exit_transition_if_FSM_triggers_an_EventExit () {
                var state = Substitute.For<IState>();
                var runFsm = new ActionRunFsm(_fsm, "a") {ParentState = state};

                runFsm.Enter();
                _fsm.EventExit.Invoke();
                runFsm.Update();
                
                state.Received(1).Transition("a");
            }

            [Test]
            public void It_should_not_trigger_transition_again_after_enter_is_called () {
                var state = Substitute.For<IState>();
                var runFsm = new ActionRunFsm(_fsm, "a") {ParentState = state};

                runFsm.Enter();
                _fsm.EventExit.Invoke();
                runFsm.Update();
                runFsm.Exit();
                runFsm.Enter();
                runFsm.Update();

                state.Received(1).Transition("a");
            }
        }
    }
}