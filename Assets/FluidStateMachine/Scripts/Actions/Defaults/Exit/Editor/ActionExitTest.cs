using NUnit.Framework;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionExitTest {
        public class UpdateMethod {
            [Test]
            public void It_should_trigger_the_Exit_action () {
                var triggered = false;
                var actionUpdate = new ActionExit((action) => triggered = true);

                actionUpdate.Exit();
                
                Assert.IsTrue(triggered);
            }
        }
    }
}