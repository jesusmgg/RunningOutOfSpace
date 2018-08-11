using UnityEngine;

namespace Cattle.Input
{
    public class PlayerInput : BaseInput
    {
        void Update()
        {
            Direction = new Vector2
            {
                x = UnityEngine.Input.GetAxis("Horizontal"),
                y = UnityEngine.Input.GetAxis("Vertical")
            };
        }

        public override bool GetButton(string button)
        {
            return UnityEngine.Input.GetButton(button);
        }
        
        public override bool GetButtonDown(string button)
        {
            return UnityEngine.Input.GetButtonDown(button);
        }
        
        public override bool GetButtonUp(string button)
        {
            return UnityEngine.Input.GetButtonUp(button);
        }
    }
}
