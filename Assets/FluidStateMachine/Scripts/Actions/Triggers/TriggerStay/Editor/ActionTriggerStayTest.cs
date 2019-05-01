using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionTriggerStayTest {
        public class InitMethod : ActionTriggerStayTest {
            private GameObject _owner;
            private ActionTriggerStay _triggerStay;

            [SetUp]
            public void BeforeEach () {
                _owner = new GameObject();
                _triggerStay = new ActionTriggerStay("Player", () => {});
                _triggerStay.ParentState = Substitute.For<IState>();
                _triggerStay.ParentState.ParentFsm.Owner.Returns(_owner);
            }

            [TearDown]
            public void AfterEach () {
                Object.DestroyImmediate(_owner);
            }
            
            [Test]
            public void It_should_attach_a_monitor_if_none_is_provided () {
                _triggerStay.Enter();
                
                Assert.IsTrue(_owner.GetComponent<ITriggerMonitor>() != null);
            }
            
            [Test]
            public void It_should_allow_overriding_the_monitor () {
                var monitor = _owner.AddComponent<ActionTriggerMonitor>();
                _triggerStay.Monitor = monitor;
                _triggerStay.Enter();
                
                Assert.AreEqual(monitor, _owner.GetComponent<ITriggerMonitor>());
            }

            [Test]
            public void It_should_not_add_a_second_monitor_if_already_on_the_owner () {
                var parentState = _triggerStay.ParentState;
                var actionTriggerAlt = new ActionTriggerStay("Player", () => {});
                actionTriggerAlt.ParentState = parentState;
                
                _triggerStay.Enter();
                actionTriggerAlt.Enter();
                
                Assert.AreEqual(1, _owner.GetComponents<ITriggerMonitor>().Length);
            }
        }
        
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

                _actionTrigger = new ActionTriggerStay("Player", () => _result = true) {
                    Monitor = _monitor
                };
                _actionTrigger.Enter();

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
