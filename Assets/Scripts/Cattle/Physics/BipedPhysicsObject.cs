using UnityEngine;
using Cattle.Input;

namespace Cattle.Physics
{
    public class BipedPhysicsObject : BasePhysicsObject
    {
        [Header("Biped")]
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;
        
        BaseInput input;

        void Awake()
        {
            input = GetComponent<BaseInput>();
        }

        protected override void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            move.x = input.Direction.x;

            if (input.GetButtonDown("Jump") && Grounded)
            {
                velocity.y = jumpTakeOffSpeed;
            }
            else if (input.GetButtonUp("Jump"))
            {
                if (velocity.y > 0)
                {
                    velocity.y *= 0.5f;
                }
            }
            
            TargetVelocity = move * maxSpeed;
        }
    }
}