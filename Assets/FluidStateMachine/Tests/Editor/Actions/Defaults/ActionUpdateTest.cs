using NUnit.Framework;

namespace CleverCrow.Fluid.FSMs.Editors {
    public class ActionUpdateTest {
        public class UpdateMethod {
            [Test]
            public void It_should_trigger_the_Update_action () {
                var triggered = false;
                var actionUpdate = new ActionUpdate((action) => triggered = true);

                actionUpdate.Update();

                Assert.IsTrue(triggered);
            }
        }
    }
}