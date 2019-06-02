using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.Fluid.FSMs.Examples {
    public class PressurePlate : MonoBehaviour {
        private IFsm _fsm;
        const string PLAYER_TAG = "Player";
  
        public UnityEvent onEnter;

        private enum PlateState {
            Neutral,
            Pressed,
        }

        private void Start () {
            _fsm = new FsmBuilder()
                .Owner(gameObject)
                .Default(PlateState.Neutral)
                .State(PlateState.Neutral, (neutral) => {
                    neutral
                        .SetTransition("enter", PlateState.Pressed)
                        .SetAnimatorBool("pressure", false)
                        .TriggerEnter(PLAYER_TAG, (action) => {
                            action.Transition("enter");
                        });
                })
                .State(PlateState.Pressed, (pressed) => {
                    pressed
                        .SetTransition("leave", PlateState.Neutral)
                        .SetAnimatorBool("pressure", true)
                        .Enter((action) => onEnter.Invoke())
                        .TriggerExit(PLAYER_TAG, (action) => {
                            action.Transition("leave");
                        });
                })
                .Build();
        }

        private void Update () {
            _fsm.Tick();
        }
    }
}