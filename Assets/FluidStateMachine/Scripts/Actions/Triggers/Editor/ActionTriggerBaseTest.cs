using System;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionTriggerBaseTest {
        private class TriggerTest : ActionTriggerBase {
            public TriggerTest (string tag, Action update) : base(tag, update) {
            }
        }
        
        public class MonitorProperty {
            private GameObject _owner;
            private TriggerTest _trigger;

            [SetUp]
            public void BeforeEach () {
                _owner = new GameObject();
                _trigger = new TriggerTest("player", () => {});
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
                var actionTriggerAlt = new TriggerTest("a", () => {});
                actionTriggerAlt.ParentState = parentState;
                
                _trigger.PopulateMonitor();
                actionTriggerAlt.PopulateMonitor();
                
                Assert.AreEqual(1, _owner.GetComponents<ITriggerMonitor>().Length);
            }
        }
    }
}