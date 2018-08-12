using UnityEngine;
using Cattle.States;
using System.Collections.Generic;
using System.Collections;

namespace Cattle
{
    public class AI : MonoBehaviour
    {
        #region SerializableField
        [SerializeField] private GameObject borderLeft;
        [SerializeField] private GameObject borderRight;
        [SerializeField] private GameObject obstacleRight;
        [SerializeField] private GameObject obstacleLeft;
        [SerializeField] private GameObject jumpRight;
        [SerializeField] private GameObject jumpLeft;
        [SerializeField] private GameObject platformRight;
        [SerializeField] private GameObject platformLeft;
        [SerializeField] private GameObject platformRightEnd;
        [SerializeField] private GameObject platformLeftEnd;
        [SerializeField] private GameObject BlockUP;

        [SerializeField] private float distanceBorder;
        [SerializeField] private float distanceObstacle;
        [SerializeField] private float distanceJump;
        [SerializeField] private float distancePlatform;
        [SerializeField] private float distanceBlock;
        #endregion

        #region PrivateField
        private BaseCollisionController collisionController;
        private StateManager stateManager;
        private GameObject player;
        private List<GameObject> collisions = new List<GameObject>();
        #endregion

        private void Awake()
        {
            collisionController = GetComponent<BaseCollisionController>();
            stateManager = GetComponent<StateManager>();
            player = GameObject.FindWithTag("Player");
        }

        private void Start()
        {
            Debug.Log(stateManager.activeState);
            StartCoroutine(Loop());
            StartCoroutine(OcasionalShoot());
        }

        private IEnumerator OcasionalShoot()
        {
            yield return new WaitForSeconds(Random.Range(3f, 5f));
            stateManager.SwitchState(new ShootState(stateManager));
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(borderLeft.transform.position, Vector2.down * distanceBorder, Color.cyan);
            Debug.DrawRay(borderRight.transform.position, Vector2.down * distanceBorder, Color.cyan);
            Debug.DrawRay(obstacleLeft.transform.position, Vector2.left * distanceObstacle, Color.red);
            Debug.DrawRay(obstacleRight.transform.position, Vector2.right * distanceObstacle, Color.red);
            Debug.DrawRay(jumpRight.transform.position, Vector2.right * distanceJump, Color.yellow);
            Debug.DrawRay(jumpLeft.transform.position, Vector2.left * distanceJump, Color.yellow);
            Debug.DrawRay(platformRight.transform.position, platformRightEnd.transform.position - platformRight.transform.position, Color.green);
            Debug.DrawRay(platformLeft.transform.position, platformLeftEnd.transform.position - platformLeft.transform.position, Color.green);
            Debug.DrawRay(BlockUP.transform.position, Vector2.up * distanceBlock, Color.blue);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            collisions.Add(collision.gameObject);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            collisions.Remove(collision.gameObject);
        }

        private IEnumerator Loop()
        {
            while (true)
            {
                var playerPosition = player.transform.position - transform.position;

                var borderLeftRay = Physics2D.Raycast(borderLeft.transform.position, Vector2.down, distanceBorder);
                var borderRightRay = Physics2D.Raycast(borderRight.transform.position, Vector2.down, distanceBorder);

                var obstacleLeftRay = Physics2D.Raycast(obstacleLeft.transform.position, Vector2.left, distanceObstacle);
                var obstacleRightRay = Physics2D.Raycast(obstacleRight.transform.position, Vector2.right, distanceObstacle);

                var jumpRightRay = Physics2D.Raycast(borderRight.transform.position, Vector2.right, distanceJump);
                var jumpLeftRay = Physics2D.Raycast(borderLeft.transform.position, Vector2.left, distanceJump);

                var platformRightRay = Physics2D.Raycast(platformRight.transform.position, (platformRightEnd.transform.position - platformRight.transform.position).normalized, (platformRightEnd.transform.position - platformRight.transform.position).magnitude);
                var platformLefttRay = Physics2D.Raycast(platformLeft.transform.position, (platformLeftEnd.transform.position - platformLeft.transform.position).normalized, (platformLeftEnd.transform.position - platformLeft.transform.position).magnitude);

                var BlockUpRay = Physics2D.Raycast(BlockUP.transform.position, Vector2.up, distanceBlock);


                if (Mathf.Abs(player.transform.position.x - transform.position.x) < 4 /*&& Mathf.Abs(player.transform.position.y - transform.position.y) < 1*/ &&  (stateManager.activeState.GetType() != typeof(JumpRightState) || stateManager.activeState.GetType() != typeof(JumpLeftState)))
                {
                    if (player.transform.position.y - transform.position.y < 1)
                    {
                        stateManager.SwitchState(new ShootState(stateManager));
                    }
                }
                else
                {
                    if(player.transform.position.y > (transform.position.y + 1))
                    {
                        if(platformLefttRay.collider && !obstacleLeftRay.collider && !platformRightRay.collider && player.transform.position.x < transform.position.x && !BlockUpRay.collider)
                        {
                            if (stateManager.activeState.GetType() != typeof(JumpLeftState))
                            {
                                stateManager.SwitchState(new JumpLeftState(stateManager));
                            }
                            yield return new WaitForSeconds(.6f);
                        }
                        if(platformRightRay.collider && !obstacleRightRay.collider && !platformLefttRay.collider && player.transform.position.x > transform.position.x && !BlockUpRay.collider)
                        {
                            if(stateManager.activeState.GetType() != typeof(JumpRightState))
                            {
                                stateManager.SwitchState(new JumpRightState(stateManager));
                            }
                            yield return new WaitForSeconds(.6f);
                        }
                    }

                    if (player.transform.position.x > transform.position.x)
                    {
                        if (obstacleRightRay)
                        {
                            stateManager.SwitchState(new JumpRightState(stateManager));
                        }
                        else
                        {
                            if (borderRightRay.collider)
                            {
                                if (stateManager.activeState.GetType() != typeof(WalkRightState))
                                {
                                    stateManager.SwitchState(new WalkRightState(stateManager));
                                }
                            }
                            else
                            {
                                if (jumpRightRay.collider)
                                {
                                    Debug.Log(collisions.Count);
                                    if (stateManager.activeState.GetType() != typeof(JumpRightState))
                                    {
                                        stateManager.SwitchState(new JumpRightState(stateManager));
                                    }
                                    yield return new WaitForSeconds(.4f);
                                }
                                else
                                {
                                    stateManager.SwitchState(new BeginState(stateManager));
                                }
                            }
                        }
                    }
                    else
                    {
                        if (/*jumpLeftRay.collider &&*/ obstacleLeftRay)
                        {
                            stateManager.SwitchState(new JumpLeftState(stateManager));
                        }
                        else
                        {
                            if (borderLeftRay.collider)
                            {
                                if (stateManager.activeState.GetType() != typeof(WalkLeftState))
                                {
                                    stateManager.SwitchState(new WalkLeftState(stateManager));
                                }
                            }
                            else
                            {
                                if (jumpLeftRay.collider)
                                {
                                    if (stateManager.activeState.GetType() != typeof(JumpLeftState))
                                    {
                                        stateManager.SwitchState(new JumpLeftState(stateManager));
                                    }
                                    yield return new WaitForSeconds(.4f);
                                }
                                else
                                {
                                    stateManager.SwitchState(new BeginState(stateManager));
                                }
                            }
                        }
                    }
                }
                yield return null;
            }
        }
    }
}