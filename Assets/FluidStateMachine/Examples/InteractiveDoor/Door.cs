using System;
using UnityEngine;

namespace CleverCrow.FluidStateMachine.Examples {
    public class Door : MonoBehaviour {
        private IFsm _door;
        private IFsm _lock;

        public bool Open { private get; set; }
        public bool Close { private get; set; }
        public bool ToggleLock { private get; set; }

        private enum DoorState {
            Opened,
            Closed,
        }

        private enum LockState {
            Locked,
            Unlocked,
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
                            if (Open && _lock.CurrentState.Id.Equals(LockState.Unlocked)) {
                                action.Transition("open");
                            }
                        });
                })
                .Build();
            
            _lock = new FsmBuilder()
                .Owner(gameObject)
                .Default(LockState.Unlocked)
                .State(LockState.Locked, (locked) => {
                    locked.SetTransition("unlock", LockState.Unlocked)
                        .SetAnimatorBool("lock", true)
                        .Update((action) => {
                            if (ToggleLock) action.Transition("unlock");
                        });
                })
                .State(LockState.Unlocked, (unlocked) => {
                    unlocked.SetTransition("lock", LockState.Locked)
                        .SetAnimatorBool("lock", false)
                        .Update((action) => {
                            if (ToggleLock && _door.CurrentState.Id.Equals(DoorState.Closed)) {
                                action.Transition("lock");
                            }
                        });
                })
                .Build();
        }

        private void Update () {
            _door.Tick();
            _lock.Tick();

            Open = false;
            Close = false;
            ToggleLock = false;
        }
    }
}