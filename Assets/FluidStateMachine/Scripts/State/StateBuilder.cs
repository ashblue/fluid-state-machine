using System;

namespace CleverCrow.FluidStateMachine.Editors {
    public class StateBuilder {
        private Enum _id;
        
        public StateBuilder (Enum id) {
            _id = id;
        }

        public IState Build () {
            return new State(_id);
        }
    }
}
