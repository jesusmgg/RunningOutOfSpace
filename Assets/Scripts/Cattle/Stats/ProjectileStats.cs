using UnityEngine;

namespace Cattle.Stats
{
    public class ProjectileStats : BaseStats
    {
        [Header("Projectile")]
        
        public float range;
        
        [Range(0.0f, 1.0f)]
        public float initialSpeedMultiplier; // Set this when instantiating the projectile
    }
}