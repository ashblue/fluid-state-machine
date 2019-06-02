using System;

namespace CleverCrow.Fluid.FSMs {
    public interface ITransition {
        string Name { get; }
        Enum Target { get; }
    }
}