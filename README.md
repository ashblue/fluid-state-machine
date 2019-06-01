# Fluid State Machine

Fluid State Machine is a Unity plugin aimed at creating state machines in pure code. It allows state actions to be re-used and customized on a per-project basis.

* Extendable, write your own re-usable state actions
* Heavily tested with TDD
* Open source and free

Get the [latest release](https://github.com/ashblue/fluid-state-machine/releases).

## Support

Join the [Discord Community](https://discord.gg/8QHFfzn) if you have questions or need help.

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

## Table of Contents

* [Action Library](#action-library)
  + [Defaults](#defaults)
    - [Enter](#enter)
    - [Exit](#exit)
    - [Update](#update)
    - [RunFsm](#runfsm)
  + [Triggers](#triggers)
    - [Enter](#enter-1)
    - [Exit](#exit-1)
    - [Stay](#stay)
  + [Animators](#animators)
    - [Set Animator Bool](#set-animator-bool)
    - [Set Animator Float](#set-animator-float)
    - [Set Animator Int](#set-animator-int)
    - [Set Animator Trigger](#set-animator-trigger)
* [Creating Custom Actions](#creating-custom-actions)
* [Development](#development)

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
        state.Enter((action) => Debug.Log("Nested FSM triggered"));
        // This will notify the fsm that triggered nestedFsm to stop running it
        state.Update((action) => action.ParentState.ParentFsm.Exit());
    })
    .Build();

var fsm = new FsmBuilder()
    .Default(StateId.A)
    .State(StateId.A, (state) => {
        state.SetTransition("next", StateId.B);
        // First argument is the transition triggered when `nestedFsm.Exit()` is detected
        state.RunFsm("next", nestedFsm);
    })
    .State(StateId.B, (state) => {
        state.Enter((action) => Debug.Log("Success"));
    })
    .Build();
```

### Triggers

Hook's Unity's collider trigger system. Note that a collider component set to trigger must be included in order for this to work.

#### Enter

Logic fired when trigger is entered with a specific tag.

```c#
.State(MyEnum.MyState, (state) => {
    state.TriggerEnter("Player", (action) => Debug.Log("Code goes here"));
})
```

#### Exit

Logic fired when trigger is exited with a specific tag.

```c#
.State(MyEnum.MyState, (state) => {
    state.TriggerExit("Player", (action) => Debug.Log("Code goes here"));
})
```

#### Stay

Logic fired when trigger is exited with a specific tag.

```c#
.State(MyEnum.MyState, (state) => {
    state.TriggerExit("Player", (action) => Debug.Log("Code goes here"));
})
```

### Animators

Talks to the current Animator. Note that an Animator component must be included on the passed GameObject owner.

#### Set Animator Bool

Sets an animator bool by string.

```c#
.State(MyEnum.MyState, (state) => {
    state.SetAnimatorBool("myBool", true);
})
```

#### Set Animator Float

Sets an animator float by string.

```c#
.State(MyEnum.MyState, (state) => {
    state.SetAnimatorFloat("myFloat", 2.2);
})
```

#### Set Animator Int

Sets an animator int by string.

```c#
.State(MyEnum.MyState, (state) => {
    state.SetAnimatorInt("myInt", 7);
})
```

#### Set Animator Trigger

Sets an animator trigger by string.

```c#
.State(MyEnum.MyState, (state) => {
    state.SetAnimatorTrigger("myInt");
})
```

## Creating Custom Actions

Here we'll cover how to create a custom action and use it in a way that gets free updates from this library. It's important you create new actions this way to prevent new versions from causing errors.


The first thing you'll need to do is create a **custom action**.

```c#
using UnityEngine;
using CleverCrow.FluidStateMachine;

public class MyAction : ActionBase {
    public MyAction (string newName) {
        Name = newName;
    }

    // Triggers when entering the state
    protected override void OnEnter () {
        Debug.Log($"Custom action {Name} activated");
    }
    
    // Triggers when exiting the state
    protected override void OnExit () {
    }
    
    // Runs every time `Fsm.Tick()` is called
    protected override void OnUpdate () {
    }
}
```

After the custom action is complete you'll need to create a **state builder** that inherits from the default state builder class.

```c#
using CleverCrow.FluidStateMachine;

public class CustomStateBuilder : StateBuilderBase<CustomStateBuilder> {
    public CustomStateBuilder MyAction (string newName) {
        _actions.Add(new MyAction(newName));
        return this;
    }
}
```

The state builder must then be plugged into an **FSM builder** class to properly encapsulate newly created states.

```c#
using CleverCrow.FluidStateMachine;

public class FsmBuilderCustom : FsmBuilderBase<FsmBuilderCustom, CustomStateBuilder> {
}
```

You've created a custom extendable FSM and state class that can be used anywhere in your code base. Try it out with a snippet like this.

```c#
using UnityEngine;
using CleverCrow.FluidStateMachine;

public class FsmBuilderCustomUsage : MonoBehaviour {
    private enum StateId {
        A,
    }
    
    private void Awake () {
        var fsmBuilder = new FsmBuilderCustom()
            .State(StateId.A, (state) => {
                state
                    .MyAction("custom name")
                    .Update((action) => { });
            });
        
        var fsm = fsmBuilder.Build();
        fsm.Tick();
    }
}
```

## Development

If you want to work on the code in this repo you'll need to install Node.js and Git. Then run the following command to setup Node.js from the repo's root.

```bash
npm install
```

### Making Commits

All commits should be made using [Commitizen](https://github.com/commitizen/cz-cli) (which is automatically installed when running `npm install`). Commits are automatically compiled to version numbers on release so this is very important. PRs that don't have Commitizen based commits will be rejected.

To make a commit type the following into a terminal from the root.

```bash
npm run commit
```
