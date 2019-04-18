using NSubstitute;
using NUnit.Framework;
using UnityEditor.Animations;
using UnityEngine;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionSetAnimatorFloatTest {
        private GameObject _go;

        [SetUp]
        public void BeforeEach () {
            _go = new GameObject();
        }

        [TearDown]
        public void AfterEach () {
            Object.DestroyImmediate(_go);
        }
        
        public class EnterMethod : ActionSetAnimatorFloatTest {
            [Test]
            public void It_should_set_an_Animator_int () {
                var fsm = Substitute.For<IFsm>();
                fsm.Owner.Returns(_go);
                var state = Substitute.For<IState>();
                state.ParentFsm.Returns(fsm);

                var param = new AnimatorControllerParameter {
                    name = "a",
                    type = AnimatorControllerParameterType.Float
                };

                var animator = _go.AddComponent<Animator>();
                var animatorCtrl = new AnimatorController {parameters = new[] {param}};
                animatorCtrl.AddLayer("Default");
                animator.runtimeAnimatorController = animatorCtrl;

                var action = new ActionSetAnimatorFloat("a", 1) {ParentState = state};

                action.Enter();

                Assert.AreEqual(1, animator.GetFloat("a"));
            }
        }
    }
}