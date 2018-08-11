using UnityEngine;
using Cattle.Input;
using Cattle.Physics;

namespace Cattle.Animation
{
    public class BipedAnimationController : BaseAnimationController
    {
        BaseInput input;
        BipedPhysicsObject physicsObject;

        protected override void Awake()
        {
            base.Awake();

            input = GetComponent<BaseInput>();
            physicsObject = GetComponent<BipedPhysicsObject>();
        }

        void Update()
        {
            bool flipSprite = spriteRenderer.flipX ? input.Direction.x > 0.01f : input.Direction.x < -0.01f;
            if (flipSprite)
            {
                spriteRenderer.flipX = !spriteRenderer.flipX;
            }
            
            animator.SetBool("grounded", physicsObject.Grounded);
            animator.SetFloat("velocityX", Mathf.Abs(physicsObject.Velocity.x));
            animator.SetFloat("velocityY", physicsObject.Velocity.y);
            animator.SetBool("jump", input.GetButtonDown("Jump"));
        }
    }
}