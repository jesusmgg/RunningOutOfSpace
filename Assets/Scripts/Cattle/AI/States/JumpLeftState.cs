using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cattle.Interfaces;

namespace Cattle.States
{
    public class JumpLeftState : IStateBase
    {
        private StateManager manager;

        public JumpLeftState(StateManager manager)
        {
            this.manager = manager;
        }

        public void StateUpdate()
        {

        }
    }
}