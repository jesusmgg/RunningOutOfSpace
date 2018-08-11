using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cattle.Interfaces;

namespace Cattle
{
    public class JumpRightState : IStateBase
    {
        private StateManager manager;

        public JumpRightState(StateManager manager)
        {
            this.manager = manager;
        }

        public void StateUpdate()
        {

        }
    }
}
