using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionRunFsmTest {
        private IFsm _fsm;
        
        [SetUp]
        public void BeforeEach () {
            var unityEvent = new UnityEvent();
            _fsm = Substitute.For<IFsm>();
            _fsm.EventExit.Returns(unityEvent);
        }
        
        public class EnterMethod : ActionRunFsmTest {
            [Test]
            public void It_should_trigger_Reset_on_the_fsm () {
                var runFsm = new ActionRunFsm("a", _fsm);

                runFsm.Enter();

                _fsm.Received(1).Reset();
            }
        }
        
        public class UpdateMethod : ActionRunFsmTest {
            [Test]
            public void It_should_tick_the_nested_fsm () {
                var runFsm = new ActionRunFsm("a", _fsm);
                
                runFsm.Update();
                
                _fsm.Received(1).Tick();
            }

            [Test]
            public void It_should_trigger_an_Exit_transition_if_FSM_triggers_an_EventExit () {
                var state = Substitute.For<IState>();
                var runFsm = new ActionRunFsm("a", _fsm) {ParentState = state};

                runFsm.Enter();
                _fsm.EventExit.Invoke();
                runFsm.Update();
                
                state.Received(1).Transition("a");
            }

            [Test]
            public void It_should_not_trigger_transition_again_after_enter_is_called () {
                var state = Substitute.For<IState>();
                var runFsm = new ActionRunFsm("a", _fsm) {ParentState = state};

                runFsm.Enter();
                _fsm.EventExit.Invoke();
                runFsm.Update();
                runFsm.Exit();
                runFsm.Enter();
                runFsm.Update();

                state.Received(1).Transition("a");
            }
        }

        public class IntegrationTests {
            private enum StateId {
                A,
                B,
            }
            
            public class BuilderUsage {
                [Test]
                public void It_should_not_call_Enter_on_nested_FSM_when_created () {
                    var stateEnter = false;
                    var nestedFsm = new FsmBuilder()
                        .Default(StateId.A)
                        .State(StateId.A, (state) => {
                            state.Enter((action) => stateEnter = true);
                        })
                        .Build();
                    
                    new FsmBuilder()
                        .Default(StateId.A)
                        .State(StateId.A, (state) => {
                            state.RunFsm("a", nestedFsm);
                        })
                        .Build();
                    
                    Assert.IsFalse(stateEnter);
                }
                
                [Test]
                public void It_should_call_Enter_on_nested_FSM_when_ticked () {
                    var stateEnter = false;
                    var nestedFsm = new FsmBuilder()
                        .Default(StateId.A)
                        .State(StateId.A, (state) => {
                            state.Enter((action) => stateEnter = true);
                        })
                        .Build();
                    
                    var fsm = new FsmBuilder()
                        .Default(StateId.A)
                        .State(StateId.A, (state) => {
                            state.RunFsm("a", nestedFsm);
                        })
                        .Build();
                    
                    fsm.Tick();
                    
                    Assert.IsTrue(stateEnter);
                }
                
                [Test]
                public void It_should_restart_nested_fsm_on_rerun () {
                    var nestedFsm = new FsmBuilder()
                        .Default(StateId.A)
                        .State(StateId.A, (state) => {
                            state.SetTransition("next", StateId.B);
                            state.Update((action) => action.Transition("next"));
                        })
                        .State(StateId.B, (state) => {
                            state.FsmExit();
                        })
                        .Build();
                    
                    var fsm = new FsmBuilder()
                        .Default(StateId.A)
                        .State(StateId.A, (state) => {
                            state.SetTransition("next", StateId.B);
                            state.RunFsm("next", nestedFsm);
                        })
                        .State(StateId.B, (state) => {
                            state.RunFsm("none", nestedFsm);
                        })
                        .Build();
                    
                    fsm.Tick();
                    fsm.Tick();

                    Assert.AreEqual(StateId.A, nestedFsm.CurrentState.Id);
                }
            }
        }
    }
}