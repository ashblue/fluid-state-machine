using NUnit.Framework;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionEnterTest {
        public class UpdateMethod {
            [Test]
            public void It_should_trigger_the_Enter_action () {
                var triggered = false;
                var actionUpdate = new ActionEnter((action) => triggered = true);

                actionUpdate.Enter();
                
                Assert.IsTrue(triggered);
            }
        }
    }
}