using UnityEngine;
using Cattle.States;

namespace Cattle.Input
{
    public class AIInput : BaseInput
    {
        private StateManager stateManager;

        private void Awake()
        {
            stateManager = GetComponent<StateManager>();
        }

        private void Update()
        {
            Debug.Log(stateManager.activeState.GetType());

            if (stateManager.activeState.GetType() == typeof(WalkRightState))
            {
                Direction = new Vector2
                {
                    x = 1,
                    y = 0
                };
            }

            if(stateManager.activeState.GetType() == typeof(WalkLeftState))
            {
                Direction = new Vector2
                {
                    x = -1,
                    y = 0
                };
            }
            
            if(stateManager.activeState.GetType() == typeof(BeginState))
            {
                Direction = new Vector2
                {
                    x = 0,
                    y = 0
                };
            }
        }

        public override bool GetButton(string button)
        {
            return false;
        }

        public override bool GetButtonDown(string button)
        {
            return false;
        }

        public override bool GetButtonUp(string button)
        {
            return false;
        }
    }
}
