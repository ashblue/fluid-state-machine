# Fluid State Machine

Fluid State Machine is a Unity plugin aimed at creating state machines in pure code. It allows state actions to be re-used and customized on a per-project basis.

* Extendable, write your own re-usable state actions
* Heavily tested with TDD
* Open source and free

Get the [latest release](https://github.com/ashblue/fluid-state-machine/releases).

## Getting Started

State machines are formatted as so. For example here we have a door that demonstrates a simple open and close mechanism.

```c#
using UnityEngine;
using CleverCrow.FluidStateMachine;

public class Door : MonoBehaviour {
    private IFsm _door;
    public bool Open { private get; set; }

    public enum DoorState {
        Opened,
        Closed,
    }

    private void Start () {
        _door = new FsmBuilder()
            .Owner(gameObject)
            .Default(DoorState.Closed)
            .State(DoorState.Closed, (close) => {
                close.SetTransition("open", DoorState.Opened)
                    .Update((action) => {
                        if (Open) action.Transition("open");
                    });
            })
            .State(DoorState.Opened, (open) => {
                open.SetTransition("close", DoorState.Closed)
                    .Update((action) => {
                        if (!Open) action.Transition("close");
                    });
            })
            .Build();
    }

    private void Update () {
        // Update the state machine every frame
        _door.Tick();
    }
}
```

### Examples

More complex usage examples can be found in `Assets/FluidStateMachine/Examples` you'll find multiple example projects and code snippets. If you plan on running any of the example scenes you'll want to read `Assets/FluidStateMachine/Examples/README.md` to add any missing dependencies.

### Releases

To get the latest build simply grab a copy from the [releases](https://github.com/ashblue/fluid-state-machine/releases) page. If you're using Node.js you can keep this package up to date by installing it with the following code via NPM. If you use the NPM package it's strongly recommended to exclude the built files `Assets/Plugins/FluidStateMachine` from version control.

CODE COMING SOON

## Action Library

Pre-made actions included in this library are as follows.

### Defaults

Actions targeted at hooking the default state machine lifecycle.

#### Enter

Triggers whenever a state is initially entered.

```c#
.State(MyEnum.MyState, (state) => {
    state.Enter((action) => Debug.Log("Code goes here"));
})
```

#### Exit

Triggers whenever a state is exited.

```c#
.State(MyEnum.MyState, (state) => {
    state.Exit((action) => Debug.Log("Code goes here"));
})
```

#### Update

Every frame a FSM's `Fsm.Tick()` method is called and the state is active this will run.

```c#
.State(MyEnum.MyState, (state) => {
    state.Update((action) => Debug.Log("Code goes here"));
})
```

#### RunFsm

Used to run a nested FSM inside of a state. This action will continue running until the nested FSM triggers an Exit event through `Fsm.Exit()`. When exit is triggered the passed transition will automatically fire.

```c#
var nestedFsm = new FsmBuilder()
    .Default(OtherStateId.A)
    .State(OtherStateId.A, (state) => {
        state.Enter((action) => stateEnter = true);
    })
    .Build();

var fsm = new FsmBuilder()
    .Default(StateId.A)
    .State(StateId.A, (state) => {
        state.SetTransition("next", StateId.B);
        state.RunFsm("next", nestedFsm);
    })
    // Runs when the nested FSM is complete
    .State(StateId.B, (state) => {
        state.Enter(() => Debug.Log("Success"));
    })
    .Build();
})
```

### Triggers

Hook's Unity's collider trigger system. Note that a collider component set to trigger must be included in order for this to work.

#### Enter

#### Exit

#### Stay

### Animators

Talks to the current Animator. Note that an Animator component must be included on the passed GameObject owner.

#### Set Animator Bool

#### Set Animator Float

#### Set Animator Int

#### Set Animator Trigger

## Creating re-usable FSMs

## Creating Custom Actions

Here we'll cover how to create a custom action and use it in a way that gets free updates from this library.

Create a new action.

Create a custom action builder.

Create a custom fsm builder.

How to use it.


