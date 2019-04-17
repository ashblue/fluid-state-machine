using NUnit.Framework;
using UnityEngine;

namespace CleverCrow.FluidStateMachine.Editors {
    public class FsmBuilderTest {
        private FsmBuilder _builder;
        private GameObject _owner;
        
        private enum StateEnum {
            A,
        }
        
        [SetUp]
        public void BeforeEach () {
            _owner = new GameObject();
            _builder = new FsmBuilder().Owner(_owner);
        }

        [TearDown]
        public void AfterEach () {
            Object.DestroyImmediate(_owner);
        }
        
        public class BuildMethod : FsmBuilderTest {
            [Test]
            public void It_should_create_an_fsm () {
                var fsm = _builder.Build();

                Assert.IsTrue(fsm is IFsm);
            }
        }
        
        public class DefaultMethod : FsmBuilderTest {
            [Test]
            public void It_should_set_the_default_start_state () {
                var fsm = _builder
                    .Default(StateEnum.A)
                    .State(StateEnum.A, (a) => {})
                    .Build();
                
                Assert.AreEqual(fsm.GetState(StateEnum.A), fsm.CurrentState);
            }
        }
        
        public class StateMethod : FsmBuilderTest {            
            [Test]
            public void It_should_add_a_state_with_an_enum_ID () {
                var fsm = _builder.State(StateEnum.A, (a) => {}).Build();
                var state = fsm.GetState(StateEnum.A);
                
                Assert.AreEqual(StateEnum.A, state.Id);
            }

            [Test]
            public void It_should_trigger_a_callback_with_a_StateBuilder_argument () {
                StateBuilder stateBuilder = null;
                _builder
                    .State(StateEnum.A, (state) => stateBuilder = state)
                    .Build();
                
                Assert.IsNotNull(stateBuilder);
            }
        }
    }
}