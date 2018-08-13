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
            this.manager.StartCoroutine(ExitTime());
        }

        private IEnumerator ExitTime()
        {
            yield return new WaitForSeconds(5f);
            manager.SwitchState(new JumpRightState(manager));
        }

        public void StateUpdate()
        {
            //manager.SwitchState(new BeginState(manager));
        }
    }
}

