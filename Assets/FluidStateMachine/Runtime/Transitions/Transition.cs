using System;

namespace CleverCrow.Fluid.FSMs {
    public class Transition : ITransition {
        public string Name { get; }
        public Enum Target { get; }
        
        public Transition (string name, Enum target) {
            Name = name;
            Target = target;
        }
    }
}