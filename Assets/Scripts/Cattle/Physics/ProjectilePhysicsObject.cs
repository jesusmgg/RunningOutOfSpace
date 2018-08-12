using UnityEngine;
using Cattle.Weapons;

namespace Cattle.Physics
{
    public class ProjectilePhysicsObject : BasePhysicsObject
    {
        [Header("Projectile")]
        
        public float speed = 10.0f;
        public float angle = 27.0f;

        public bool rotate;
        public float rotationSpeed;

        bool fired = false;

        BaseProjectile projectile;
        SpriteRenderer spriteRenderer;

        void Awake()
        {
            projectile = GetComponent<BaseProjectile>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void Update()
        {
            base.Update();

            if (rotate)
            {
                transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * rotationSpeed);
            }
        }

        protected override void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            move.x = speed * projectile.initialSpeedMultiplier * Mathf.Cos(angle * Mathf.Deg2Rad);
            
            if (spriteRenderer.flipX) {move.x *= -1.0f;}
            
            move.x += projectile.initialVelocity.x;
            
            if (!fired)
            {
                velocity.y = speed * projectile.initialSpeedMultiplier * Mathf.Sin(angle * Mathf.Deg2Rad);
                velocity.y += projectile.initialVelocity.y;
                fired = true;
            }

            TargetVelocity = move;
        }
    }
}