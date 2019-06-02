using System;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace CleverCrow.Fluid.FSMs.Editors {
    public class ActionTriggerBaseTest {
        private class TriggerTest : ActionTriggerBase {
            public TriggerTest (string tag, Action<IAction> update) : base(tag, update) {
            }
        }
        
        public class MonitorProperty {
            private GameObject _owner;
            private TriggerTest _trigger;

            [SetUp]
            public void BeforeEach () {
                _owner = new GameObject();
                _trigger = new TriggerTest("player", (action) => {});
                _trigger.ParentState = Substitute.For<IState>();
                _trigger.ParentState.ParentFsm.Owner.Returns(_owner);
            }

            [TearDown]
            public void AfterEach () {
                Object.DestroyImmediate(_owner);
            }
            
            [Test]
            public void It_should_attach_a_monitor_if_none_is_provided () {
                _trigger.PopulateMonitor();
                
                Assert.IsTrue(_owner.GetComponent<ITriggerMonitor>() != null);
            }
            
            [Test]
            public void It_should_allow_overriding_the_monitor () {
                var monitor = _owner.AddComponent<ActionTriggerMonitor>();
                _trigger.Monitor = monitor;
                _trigger.PopulateMonitor();
                
                Assert.AreEqual(monitor, _owner.GetComponent<ITriggerMonitor>());
            }

            [Test]
            public void It_should_not_add_a_second_monitor_if_already_on_the_owner () {
                var parentState = _trigger.ParentState;
                var actionTriggerAlt = new TriggerTest("a", (action) => {});
                actionTriggerAlt.ParentState = parentState;
                
                _trigger.PopulateMonitor();
                actionTriggerAlt.PopulateMonitor();
                
                Assert.AreEqual(1, _owner.GetComponents<ITriggerMonitor>().Length);
            }
        }
        
        public abstract class UpdateMethod {
            private ICollider _collider;
            private ITriggerMonitor _monitor;
            private ActionTriggerBase _actionTrigger;
            private bool _result;

            private class EventTrigger : UnityEvent<ICollider> {
            }

            protected abstract UnityEvent<ICollider> GetEventTrigger (ITriggerMonitor monitor);
            protected abstract ActionTriggerBase GetNewActionTrigger (string tag, Action<IAction> action);
        
            [SetUp]
            public void BeforeEach () {
                _result = false;
                
                _monitor = Substitute.For<ITriggerMonitor>();
                GetEventTrigger(_monitor).Returns(new EventTrigger());

                _actionTrigger = GetNewActionTrigger("Player", (action) => _result = true);
                _actionTrigger.Monitor = _monitor;
                _actionTrigger.Enter();

                _collider = Substitute.For<ICollider>();
                _collider.CompareTag("Player").Returns(true);
            }

            [Test]
            public void It_should_trigger_update_logic_if_trigger_fired () {
                GetEventTrigger(_monitor).Invoke(_collider);
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
                GetEventTrigger(_monitor).Invoke(_collider);
                _actionTrigger.Update();
                _result = false;
                _actionTrigger.Update();

                Assert.IsFalse(_result);
            }

            [Test]
            public void It_should_not_trigger_if_there_isnt_a_matching_tag_on_the_collider () {
                _collider.CompareTag("Player").Returns(false);
                GetEventTrigger(_monitor).Invoke(_collider);
                _actionTrigger.Update();

                Assert.IsFalse(_result);
            }
        }
    }
}