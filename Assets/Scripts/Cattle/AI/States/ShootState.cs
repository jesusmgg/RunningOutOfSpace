using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cattle.Interfaces;

namespace Cattle.States
{
    public class ShootState : IStateBase
    {
        private StateManager manager;

        public ShootState(StateManager manager)
        {
            this.manager = manager;
        }

        public void StateUpdate()
        {
            manager.SwitchState(new BeginState(manager));
        }
    }
}

