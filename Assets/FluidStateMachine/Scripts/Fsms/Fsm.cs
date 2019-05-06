using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CleverCrow.FluidStateMachine {
    [Serializable]
    public class Fsm : IFsm {
        private readonly Dictionary<Enum, IState> _stateDic = new Dictionary<Enum, IState>();
        
        public GameObject Owner { get; }
        public IState CurrentState { get; private set; }
        public IState DefaultState { get; set; }
        public UnityEvent EventExit { get; } = new UnityEvent();

        public Fsm (GameObject owner) {
            Owner = owner;
        }

        public IState GetState (Enum id) {
            return _stateDic[id];
        }

        public void AddState (IState state) {
            _stateDic[state.Id] = state;
        }

        public void SetState (Enum id) {
            CurrentState?.Exit();
            CurrentState = GetState(id);
            CurrentState.Enter();
        }

        public void Tick () {
            CurrentState?.Update();
        }

        public void Reset () {
            SetState(DefaultState.Id);
        }

        public void Exit () {
            CurrentState.Exit();
            EventExit.Invoke();
        }
    }
}
