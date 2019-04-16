using System;
using DefaultNamespace;

namespace CleverCrow.FluidStateMachine {
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
    }
}