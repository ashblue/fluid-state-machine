using System;
using DefaultNamespace;

namespace CleverCrow.FluidStateMachine {
    /// <summary>
    /// An action triggered every frame
    /// </summary>
    public class ActionUpdate : IAction {
        private readonly Action _update;
        
        public string Name { get; }

        public ActionUpdate (string name, Action update) {
            Name = name;
            _update = update;
        }
        
        public void Update () {
            _update();
        }

        public void Enter () {
        }

        public void Exit () {
        }
    }
}