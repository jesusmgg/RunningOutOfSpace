using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cattle.States
{
    public class BeginState : Interfaces.IStateBase
    {
        private StateManager manager;

        public BeginState(StateManager manager)
        {
            this.manager = manager;
        }

        public void StateUpdate()
        {
            
        }
    }
}
