using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionTriggerStayTest {        
        public class UpdateMethod : ActionTriggerStayTest {
            private ICollider _collider;
            private ITriggerMonitor _monitor;
            private ActionTriggerStay _actionTrigger;
            private bool _result;

            private class EventTriggerStay : UnityEvent<ICollider> {
            }
        
            [SetUp]
            public void BeforeEach () {
                _result = false;
                
                _monitor = Substitute.For<ITriggerMonitor>();
                _monitor.EventTriggerStay.Returns(new EventTriggerStay());
                
                _actionTrigger = new ActionTriggerStay(_monitor, "Player", () => _result = true);

                _collider = Substitute.For<ICollider>();
                _collider.CompareTag("Player").Returns(true);
            }

            [Test]
            public void It_should_trigger_update_logic_if_trigger_fired () {
                _monitor.EventTriggerStay.Invoke(_collider);
                _actionTrigger.Update();
                
                Assert.IsTrue(_result);
            }
            
            [Test]
            public void It_should_not_trigger_update_logic_if_no_trigger_fired () {
                _actionTrigger.Update();
                
                Assert.IsFalse(_result);
            }

            [Test]
            public void It_should_not_trigger_update_unless_previous_frame_triggered_stay () {
                _monitor.EventTriggerStay.Invoke(_collider);
                _actionTrigger.Update();
                _result = false;
                _actionTrigger.Update();

                Assert.IsFalse(_result);
            }

            [Test]
            public void It_should_not_trigger_if_there_isnt_a_matching_tag_on_the_collider () {
                _collider.CompareTag("Player").Returns(false);
                _monitor.EventTriggerStay.Invoke(_collider);
                _actionTrigger.Update();

                Assert.IsFalse(_result);
            }
        }
    }
}
