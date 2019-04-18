using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using AnimatorController = UnityEditor.Animations.AnimatorController;

namespace CleverCrow.FluidStateMachine.Editors {
    public class ActionSetAnimatorBoolTest {
        private GameObject _go;

        [SetUp]
        public void BeforeEach () {
            _go = new GameObject();
        }

        [TearDown]
        public void AfterEach () {
            Object.DestroyImmediate(_go);
        }
        
        public class EnterMethod : ActionSetAnimatorBoolTest {
            [Test]
            public void It_should_set_an_Animator_bool () {
                var fsm = Substitute.For<IFsm>();
                fsm.Owner.Returns(_go);
                var state = Substitute.For<IState>();
                state.ParentFsm.Returns(fsm);

                var param = new AnimatorControllerParameter {
                    name = "a", 
                    type = AnimatorControllerParameterType.Bool
                };

                var animator = _go.AddComponent<Animator>();
                var animatorCtrl = new AnimatorController {parameters = new[] {param}};
                animatorCtrl.AddLayer("Default");
                animator.runtimeAnimatorController = animatorCtrl;

                var action = new ActionSetAnimatorBool("a", true) {ParentState = state};

                action.Enter();
                
                Assert.AreEqual(true, animator.GetBool("a"));
            }
        }
    }
}