using System;
using System.Collections.Generic;
using UnityEngine;

namespace CleverCrow.FluidStateMachine {
    [Serializable]
    public class Fsm : IFsm {
        private readonly Dictionary<Enum, IState> _stateDic = new Dictionary<Enum, IState>();
        
        public GameObject Owner { get; }
        public IState CurrentState { get; private set; }

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
    }
}