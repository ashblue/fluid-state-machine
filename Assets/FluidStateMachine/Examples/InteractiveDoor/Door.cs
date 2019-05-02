using UnityEngine;

namespace CleverCrow.FluidStateMachine.Examples {
    public class Door : MonoBehaviour {
        private IFsm _door;

        public bool open;
        public bool close;

        private enum DoorState {
            Opened,
            Closed,
        }

        private void Start () {
            _door = new FsmBuilder()
                .Owner(gameObject)
                .Default(DoorState.Closed)
                .State(DoorState.Opened, (open) => {
                    open.SetTransition("close", DoorState.Closed)
                        .SetAnimatorBool("doorOpen", true)
                        .Update((action) => {
                            if (close) action.Transition("close");
                        });
                })
                .State(DoorState.Closed, (close) => {
                    close.SetTransition("open", DoorState.Opened)
                        .SetAnimatorBool("doorOpen", false)
                        .Update((action) => {
                            if (open) action.Transition("open");
                        });
                })
                .Build();
        }

        private void Update () {
            _door.Tick();

            open = false;
            close = false;
        }
    }
}