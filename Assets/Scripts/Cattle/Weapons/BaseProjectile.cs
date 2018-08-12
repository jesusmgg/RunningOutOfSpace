using UnityEngine;

namespace Cattle.Weapons
{
    public class BaseProjectile : BaseComponent
    {
        [Range(0.0f, 1.0f)]
        public float initialSpeedMultiplier;

        public Vector2 initialVelocity;

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            throw new System.NotImplementedException();
        }
    }
}