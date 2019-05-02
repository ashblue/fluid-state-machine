using NSubstitute;
using NUnit.Framework;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionTriggerEnterTest {
        public class UpdateMethod {
            private ICollider _collider;
            private ITriggerMonitor _monitor;
            private ActionTriggerEnter _actionTrigger;
            private bool _result;

            private class EventTriggerEnter : UnityEvent<ICollider> {
            }
        
            [SetUp]
            public void BeforeEach () {
                _result = false;
                
                _monitor = Substitute.For<ITriggerMonitor>();
                _monitor.EventTriggerEnter.Returns(new EventTriggerEnter());

                _actionTrigger = new ActionTriggerEnter("Player", () => _result = true) {
                    Monitor = _monitor
                };
                _actionTrigger.Enter();

                _collider = Substitute.For<ICollider>();
                _collider.CompareTag("Player").Returns(true);
            }

            [Test]
            public void It_should_trigger_update_logic_if_trigger_fired () {
                _monitor.EventTriggerEnter.Invoke(_collider);
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
                _monitor.EventTriggerEnter.Invoke(_collider);
                _actionTrigger.Update();
                _result = false;
                _actionTrigger.Update();

                Assert.IsFalse(_result);
            }

            [Test]
            public void It_should_not_trigger_if_there_isnt_a_matching_tag_on_the_collider () {
                _collider.CompareTag("Player").Returns(false);
                _monitor.EventTriggerEnter.Invoke(_collider);
                _actionTrigger.Update();

                Assert.IsFalse(_result);
            }
        }
    }
}