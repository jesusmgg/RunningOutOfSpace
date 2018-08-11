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
            if (stateManager.activeState.GetType() == typeof(ShootState))
            {
                Direction = new Vector2
                {
                    x = 0,
                    y = 0
                };
            }
            else
            {
                if (stateManager.activeState.GetType() == typeof(WalkRightState) || stateManager.activeState.GetType() == typeof(JumpRightState))
                {
                    Direction = new Vector2
                    {
                        x = 1,
                        y = 0
                    };
                }

                if (stateManager.activeState.GetType() == typeof(WalkLeftState))
                {
                    Direction = new Vector2
                    {
                        x = -1,
                        y = 0
                    };
                }
            }


            if (stateManager.activeState.GetType() == typeof(BeginState))
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
            if (button.Equals("Jump"))
            {
                if (stateManager.activeState.GetType() == typeof(JumpRightState))
                {
                    Debug.Log("Jump");
                    return true;
                }
                else
                {
                    Debug.Log("OutOfState");
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public override bool GetButtonUp(string button)
        {
            return false;
        }
    }
}
