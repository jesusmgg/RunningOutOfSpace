using UnityEngine;
using Cattle.Input;

namespace Cattle.Physics
{
    public class BasePhysicsObject : BaseComponent
    {
        public float minGroundNormalY = .65f;
        public float gravityModifier = 1f;
        
        protected Vector2 groundNormal;
        protected Vector2 velocity;
        protected ContactFilter2D contactFilter;
        protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];

        protected const float minMoveDistance = 0.001f;
        protected const float shellRadius = 0.01f;
        
        protected Rigidbody2D rigidBody2D;
        
        public Vector2 TargetVelocity { get; set; }
        public Vector2 Velocity => velocity;
        public bool Grounded { get; set; }

        void OnEnable()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
        }

        void Start()
        {
            contactFilter.useTriggers = false;
            contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
            contactFilter.useLayerMask = true;
        }

        void Update()
        {
            TargetVelocity = Vector2.zero;
            ComputeVelocity();
        }

        protected virtual void ComputeVelocity()
        {
            TargetVelocity = Vector2.zero;
        }

        void FixedUpdate()
        {
            velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
            velocity.x = TargetVelocity.x;

            Grounded = false;

            Vector2 deltaPosition = velocity * Time.deltaTime;

            Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);

            Vector2 move = moveAlongGround * deltaPosition.x;

            Movement(move, false);

            move = Vector2.up * deltaPosition.y;

            Movement(move, true);
        }

        void Movement(Vector2 move, bool yMovement)
        {
            float distance = move.magnitude;

            if (distance > minMoveDistance)
            {
                int count = rigidBody2D.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
                for (int i = 0; i < count; i++)
                {
                    Vector2 currentNormal = hitBuffer[i].normal;
                    if (currentNormal.y > minGroundNormalY)
                    {
                        Grounded = true;
                        if (yMovement)
                        {
                            groundNormal = currentNormal;
                            currentNormal.x = 0;
                        }
                    }

                    float projection = Vector2.Dot(velocity, currentNormal);
                    if (projection < 0)
                    {
                        velocity = velocity - projection * currentNormal;
                    }

                    float modifiedDistance = hitBuffer[i].distance - shellRadius;
                    distance = modifiedDistance < distance ? modifiedDistance : distance;
                }
            }

            rigidBody2D.position = rigidBody2D.position + move.normalized * distance;
        }
    }
}