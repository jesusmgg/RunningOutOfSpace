using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cattle.Input;
using Cattle.Interfaces;

namespace Cattle
{
    public class WalkRightState : IStateBase
    {
        private StateManager manager;

        public WalkRightState(StateManager manager)
        {
            this.manager = manager;
        }

        public void StateUpdate()
        {
            
        }
    }
}
