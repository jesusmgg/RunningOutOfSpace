using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cattle.Interfaces;

namespace Cattle.States
{
    public class WalkLeftState : IStateBase
    {
        private StateManager manager;

        public WalkLeftState(StateManager manager)
        {
            this.manager = manager;
        }

        public void StateUpdate()
        {

        }
    }
}
