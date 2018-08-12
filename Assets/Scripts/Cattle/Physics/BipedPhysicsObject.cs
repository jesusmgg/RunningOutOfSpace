using Cattle.Game;
using UnityEngine;
using Cattle.Input;

namespace Cattle.Physics
{
    public class BipedPhysicsObject : BasePhysicsObject
    {
        [Header("Biped")]
        public float maxSpeed = 7;
        public float jumpTakeOffSpeed = 7;
        
        public bool useRandomSpeed = false;
        public float minRandomSpeed = 5.0f;
        public float maxRandomSpeed = 7.0f;
        
        BaseInput input;
        PlayerGameScript gameScript;

        void Awake()
        {
            input = GetComponent<BaseInput>();

            gameScript = GetComponent<PlayerGameScript>();
        }

        protected override void Start()
        {
            base.Start();

            if (useRandomSpeed)
            {
                maxSpeed = Random.Range(minRandomSpeed, maxRandomSpeed);
            }
        }

        protected override void Update()
        {
            if (gameScript != null)
            {
                usePhysics = gameScript.isAlive;
            }
            
            base.Update();
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