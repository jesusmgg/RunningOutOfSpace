using UnityEngine;
using Cattle.States;
using Cattle.Interfaces;

namespace Cattle
{
    public class StateManager : MonoBehaviour
    {
        public IStateBase activeState;
        //private static StateManager instance;

        private void Start()
        {
            activeState = new BeginState(GetComponent<StateManager>());
        }

        private void Update()
        {
            if(activeState != null)
            {
                activeState.StateUpdate();
            }
        }

        public void SwitchState(IStateBase newState)
        {
            activeState = newState;
        }
    }
}
