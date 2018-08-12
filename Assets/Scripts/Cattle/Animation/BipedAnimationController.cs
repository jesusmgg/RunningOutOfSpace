using Cattle.Game;
using UnityEngine;
using Cattle.Input;
using Cattle.Physics;

namespace Cattle.Animation
{
    public class BipedAnimationController : BaseAnimationController
    {
        BaseInput input;
        BipedPhysicsObject physicsObject;

        PlayerGameScript gameScript;

        protected override void Awake()
        {
            base.Awake();

            input = GetComponent<BaseInput>();
            physicsObject = GetComponent<BipedPhysicsObject>();
            gameScript = GetComponent<PlayerGameScript>();
        }

        void Update()
        {
            if (gameScript != null)
            {
                spriteRenderer.color = gameScript.isAlive ? Color.white : Color.clear;
            }
            
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