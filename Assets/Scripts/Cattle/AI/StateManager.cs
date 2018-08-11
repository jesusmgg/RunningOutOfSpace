using UnityEngine;
using Cattle.States;
using Cattle.Interfaces;

namespace Cattle
{
    public class StateManager : MonoBehaviour
    {
        private IStateBase activeState;
        private static StateManager instance;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        private void Start()
        {
            activeState = new BeginState(this);
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
