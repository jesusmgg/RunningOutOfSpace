using UnityEngine;
using Cattle.Weapons;

namespace Cattle.Physics
{
    public class ProjectilePhysicsObject : BasePhysicsObject
    {
        [Header("Projectile")]
        
        public float speed = 10.0f;
        public float angle = 27.0f;

        bool fired = false;

        BaseProjectile projectile;
        SpriteRenderer spriteRenderer;

        void Awake()
        {
            projectile = GetComponent<BaseProjectile>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected override void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            move.x = speed * projectile.initialSpeedMultiplier * Mathf.Cos(angle * Mathf.Deg2Rad);
            
            if (spriteRenderer.flipX) {move.x *= -1.0f;}
            
            if (!fired)
            {
                velocity.y = speed * projectile.initialSpeedMultiplier * Mathf.Sin(angle * Mathf.Deg2Rad);
                fired = true;
            }

            TargetVelocity = move;
        }
    }
}