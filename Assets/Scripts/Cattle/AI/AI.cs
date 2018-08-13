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
        private GameObject[] players;

        private bool playerUp;
        private bool playerDown;
        private bool playerRight;
        private bool playerLeft;
        #endregion

        private void Awake()
        {
            collisionController = GetComponent<BaseCollisionController>();
            stateManager = GetComponent<StateManager>();
        }

        private void Start()
        {
            Debug.Log(stateManager.activeState);
            StartCoroutine(ChangeTarget());
            StartCoroutine(OcasionalShoot());
            StartCoroutine(PlayerPosition());
        }

        private IEnumerator PlayerPosition()
        {
            while(true)
            {
                playerLeft = (player.transform.position.x - transform.position.x) < 0;
                playerRight = (player.transform.position.x - transform.position.x) >= 0;
                playerUp = (player.transform.position.y - transform.position.y + .5f) > 0;
                playerDown = (player.transform.position.y - transform.position.y - .5f) <= 0;
                Debug.Log("Up : " + playerUp + " Right : " + playerRight + " Left : " + playerLeft + " Down : " + playerDown);
                yield return null;
            }
        }

        private IEnumerator ChangeTarget()
        {
            while(true)
            {
                while(!player || !player.GetComponent<Game.PlayerGameScript>().isAlive || player == gameObject)
                {
                    players = GameObject.FindGameObjectsWithTag("Player");
                    player = players[Random.Range(0, players.Length)];
                    yield return null;
                }
                StartCoroutine(Loop());
                yield return new WaitForSeconds(5f);
                players = GameObject.FindGameObjectsWithTag("Player");
                player = players[Random.Range(0, players.Length)];
            }
        }

        private IEnumerator OcasionalShoot()
        {
            yield return new WaitForSeconds(Random.Range(10f, 15f));
            stateManager.SwitchState(new ShootState(stateManager));
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(borderLeft.transform.position, Vector2.down * distanceBorder, Color.cyan);
            Debug.DrawRay(borderRight.transform.position, Vector2.down * distanceBorder, Color.cyan);
            Debug.DrawRay(obstacleLeft.transform.position, Vector2.left * distanceObstacle, Color.red);
            Debug.DrawRay(obstacleRight.transform.position, Vector2.right * distanceObstacle, Color.red);
            Debug.DrawRay(jumpRight.transform.position, Vector2.down * distanceJump, Color.yellow);
            Debug.DrawRay(jumpLeft.transform.position, Vector2.down * distanceJump, Color.yellow);
            Debug.DrawRay(platformRight.transform.position, platformRightEnd.transform.position - platformRight.transform.position, Color.green);
            Debug.DrawRay(platformLeft.transform.position, platformLeftEnd.transform.position - platformLeft.transform.position, Color.green);
            Debug.DrawRay(BlockUP.transform.position, Vector2.up * distanceBlock, Color.blue);

            Debug.DrawLine(player.transform.position, transform.position);
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
            

            yield return new WaitForEndOfFrame();
            while (true)
            {
                Debug.Log(stateManager.activeState.GetType());


                var playerPosition = player.transform.position - transform.position;

                var borderLeftRay = Physics2D.Raycast(borderLeft.transform.position, Vector2.down, distanceBorder);
                var borderRightRay = Physics2D.Raycast(borderRight.transform.position, Vector2.down, distanceBorder);

                var obstacleLeftRay = Physics2D.Raycast(obstacleLeft.transform.position, Vector2.left, distanceObstacle);
                var obstacleRightRay = Physics2D.Raycast(obstacleRight.transform.position, Vector2.right, distanceObstacle);

                var jumpRightRay = Physics2D.Raycast(jumpRight.transform.position, Vector2.down, distanceJump);
                var jumpLeftRay = Physics2D.Raycast(jumpLeft.transform.position, Vector2.down, distanceJump);

                var platformRightRay = Physics2D.Raycast(platformRight.transform.position, (platformRightEnd.transform.position - platformRight.transform.position).normalized, (platformRightEnd.transform.position - platformRight.transform.position).magnitude);
                var platformLefttRay = Physics2D.Raycast(platformLeft.transform.position, (platformLeftEnd.transform.position - platformLeft.transform.position).normalized, (platformLeftEnd.transform.position - platformLeft.transform.position).magnitude);

                var BlockUpRay = Physics2D.Raycast(BlockUP.transform.position, Vector2.up, distanceBlock);


                stateManager.SwitchState(new BeginState(stateManager));

                

                Debug.Log(playerUp + " " + playerDown);

                if (Mathf.Abs(player.transform.position.x - transform.position.x) < 3)
                {
                    stateManager.SwitchState(new BeginState(stateManager));
                    yield return new WaitForSeconds(.5f);
                    stateManager.SwitchState(new ShootState(stateManager));
                    yield return new WaitForSeconds(1f);
                    players = GameObject.FindGameObjectsWithTag("Player");
                    player = players[Random.Range(0, players.Length)];
                    if (playerLeft && jumpRightRay.collider && borderLeftRay.collider)
                    {
                        stateManager.SwitchState(new JumpRightState(stateManager));
                        yield return new WaitForSeconds(.5f);
                    }
                    if(playerRight && jumpLeftRay.collider && borderRightRay.collider)
                    {
                        stateManager.SwitchState(new JumpLeftState(stateManager));
                        yield return new WaitForSeconds(.5f);
                    }
                }


                if(Mathf.Abs(player.transform.position.x - transform.position.x) >= 3)
                {
                    if(playerLeft && borderLeftRay.collider)
                    {
                        stateManager.SwitchState(new WalkLeftState(stateManager));
                        //yield return null;
                        yield return new WaitForSeconds(.1f);
                    }
                    if(playerRight && borderRightRay.collider)
                    {
                        stateManager.SwitchState(new WalkRightState(stateManager));
                        //yield return null;
                        yield return new WaitForSeconds(.1f);
                    }


                    if(!borderLeftRay.collider && playerLeft && jumpLeftRay.collider && obstacleLeftRay.collider)
                    {
                        stateManager.SwitchState(new JumpLeftState(stateManager));
                        yield return new WaitForSeconds(.5f);
                    }
                    if(!borderRightRay.collider && playerRight && jumpRightRay.collider && obstacleRightRay.collider)
                    {
                        stateManager.SwitchState(new JumpRightState(stateManager));
                        yield return new WaitForSeconds(.5f);
                    }


                    if(obstacleLeftRay.collider && playerLeft && jumpLeftRay.collider)
                    {
                        stateManager.SwitchState(new JumpLeftState(stateManager));
                        yield return new WaitForSeconds(.5f);
                    }
                    if(obstacleRightRay.collider && playerRight && jumpRightRay.collider)
                    {
                        stateManager.SwitchState(new JumpRightState(stateManager));
                        yield return new WaitForSeconds(.5f);
                    }


                    if(playerLeft && playerUp && platformLefttRay.collider && !obstacleLeftRay.collider)
                    {
                        stateManager.SwitchState(new JumpLeftState(stateManager));
                        yield return new WaitForSeconds(.5f);
                    }
                    if(playerRight && playerUp && platformRightRay.collider && !obstacleRightRay.collider)
                    {
                        stateManager.SwitchState(new JumpRightState(stateManager));
                        yield return new WaitForSeconds(.5f);
                    }
                }


                /*if (Mathf.Abs(player.transform.position.x - transform.position.x) < 3 &&  (stateManager.activeState.GetType() != typeof(JumpRightState) || stateManager.activeState.GetType() != typeof(JumpLeftState)))
                {
                    if ((player.transform.position.y - transform.position.y) < 1)
                    {
                        stateManager.SwitchState(new ShootState(stateManager));
                        if(Random.Range(0, 1) == 1)
                        {
                            stateManager.SwitchState(new JumpRightState(stateManager));
                        }
                        else
                        {
                            stateManager.SwitchState(new JumpLeftState(stateManager));
                        }
                    }
                }
                else
                {
                    if(player.transform.position.y > (transform.position.y + 1))
                    {
                        if(platformLefttRay.collider && !obstacleLeftRay.collider && !platformRightRay.collider && player.transform.position.x < transform.position.x && !BlockUpRay.collider)
                        {
                            //if (stateManager.activeState.GetType() != typeof(JumpLeftState))
                            //{
                                stateManager.SwitchState(new JumpLeftState(stateManager));
                            //}
                            yield return new WaitForSeconds(.6f);
                        }
                        if(platformRightRay.collider && !obstacleRightRay.collider && !platformLefttRay.collider && player.transform.position.x > transform.position.x && !BlockUpRay.collider)
                        {
                            //if(stateManager.activeState.GetType() != typeof(JumpRightState))
                            //{
                                stateManager.SwitchState(new JumpRightState(stateManager));
                            //}
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
                        if (obstacleLeftRay)
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
                }*/
                yield return null;
            }
        }
    }
}