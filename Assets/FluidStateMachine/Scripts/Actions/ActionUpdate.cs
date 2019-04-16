using System;
using DefaultNamespace;

namespace CleverCrow.FluidStateMachine {
    public class ActionUpdate : IAction {
        public string Name { get; }

        public ActionUpdate (string name, Action update) {
            Name = name;
        }
    }
}