using NUnit.Framework;

namespace CleverCrow.FluidStateMachine.Editors {
    public class FsmBuilderTest {
        private FsmBuilder _builder;
        
        [SetUp]
        public void BeforeEach () {
            _builder = new FsmBuilder();
        }
        
        public class BuildMethod : FsmBuilderTest {
            [Test]
            public void It_should_create_an_fsm () {
                _builder = new FsmBuilder();
                var fsm = _builder.Build();

                Assert.IsTrue(fsm is IFsm);
            }
        }
        
        public class StateMethod : FsmBuilderTest {
            private enum StateEnum {
                A,
            }
            
            [Test]
            public void It_should_add_a_state_with_an_enum_ID () {
                _builder = new FsmBuilder();
                var fsm = _builder.State(StateEnum.A, (a) => {}).Build();
                var state = fsm.GetState(StateEnum.A);
                
                Assert.AreEqual(StateEnum.A, state.Id);
            }

            [Test]
            public void It_should_trigger_a_callback_with_a_StateBuilder_argument () {
                _builder = new FsmBuilder();
                StateBuilder stateBuilder = null;
                _builder
                    .State(StateEnum.A, (state) => stateBuilder = state)
                    .Build();
                
                Assert.IsNotNull(stateBuilder);
            }
        }
    }
}