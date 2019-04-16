using DefaultNamespace;
using NSubstitute;
using NUnit.Framework;

namespace CleverCrow.FluidStateMachine.Editors {
    public class StateTest {
        private enum StateId {
            A,
        }
        
        public class UpdateMethod {
            [Test]
            public void It_should_call_Update_on_all_Actions () {
                var action = Substitute.For<IAction>();
                var state = new State(StateId.A);
                state.Actions.Add(action);

                state.Update();
                
                action.Received(1).Update();
            }
        }
        
        public class EnterMethod {
            [Test]
            public void It_should_call_Enter_on_all_Actions () {
                var action = Substitute.For<IAction>();
                var state = new State(StateId.A);
                state.Actions.Add(action);

                state.Enter();
                
                action.Received(1).Enter();
            }
        }

        public class ExitMethod {
            [Test]
            public void It_should_call_Exit_on_all_Actions () {
                var action = Substitute.For<IAction>();
                var state = new State(StateId.A);
                state.Actions.Add(action);

                state.Exit();
                
                action.Received(1).Exit();
            }
        }
    }
}