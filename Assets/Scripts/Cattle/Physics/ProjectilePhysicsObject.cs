using UnityEngine;
using Cattle.Stats;

namespace Cattle.Physics
{
    public class ProjectilePhysicsObject : BasePhysicsObject
    {
        [Header("Projectile")]
        
        public float speed = 10.0f;
        public float angle = 27.0f;

        bool fired = false;

        ProjectileStats stats;

        void Awake()
        {
            stats = GetComponent<ProjectileStats>();
        }

        protected override void ComputeVelocity()
        {
            Vector2 move = Vector2.zero;

            move.x = speed * stats.initialSpeedMultiplier * Mathf.Cos(angle * Mathf.Deg2Rad);

            if (!fired)
            {
                velocity.y = speed * stats.initialSpeedMultiplier * Mathf.Sin(angle * Mathf.Deg2Rad);
                fired = true;
            }

            TargetVelocity = move;
        }
    }
}