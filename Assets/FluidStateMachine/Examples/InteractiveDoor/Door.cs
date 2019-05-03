using UnityEngine;

namespace CleverCrow.FluidStateMachine.Examples {
    public class Door : MonoBehaviour {
        private IFsm _door;

        public bool Open { get; set; }
        public bool Close { get; set; }

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
                        .SetAnimatorBool("open", true)
                        .Update((action) => {
                            if (Close) action.Transition("close");
                        });
                })
                .State(DoorState.Closed, (close) => {
                    close.SetTransition("open", DoorState.Opened)
                        .SetAnimatorBool("open", false)
                        .Update((action) => {
                            if (Open) action.Transition("open");
                        });
                })
                .Build();
        }

        private void Update () {
            _door.Tick();

            Open = false;
            Close = false;
        }
    }
}