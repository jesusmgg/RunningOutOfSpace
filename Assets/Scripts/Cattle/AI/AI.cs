using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cattle.States;

namespace Cattle
{
    public class AI : MonoBehaviour
    {
        #region SerializableField
        [SerializeField] private GameObject borderLeft;
        [SerializeField] private GameObject borderRight;
        [SerializeField] private GameObject obstacleRight;
        [SerializeField] private GameObject obstacleLeft;
        [SerializeField] private float distanceBorder;
        [SerializeField] private float distanceObstacle;

        #endregion

        #region PrivateField
        private BaseCollisionController collisionController;
        private StateManager stateManager;
        private GameObject player;
        private bool movingRight = false;
        private float lastShootTime;
        #endregion

        private void Awake()
        {
            collisionController = GetComponent<BaseCollisionController>();
            stateManager = GetComponent<StateManager>();
            player = GameObject.FindWithTag("Player");
        }

        private void OnDrawGizmos()
        {
            Debug.DrawRay(borderLeft.transform.position, Vector2.down * distanceBorder, Color.cyan);
            Debug.DrawRay(borderRight.transform.position, Vector2.down * distanceBorder, Color.cyan);
            Debug.DrawRay(obstacleLeft.transform.position, Vector2.left * distanceObstacle, Color.red);
            Debug.DrawRay(obstacleRight.transform.position, Vector2.right * distanceObstacle, Color.red);
        }

        private void Update()
        {
            var playerPosition = player.transform.position - transform.position;

            var borderLeftRay = Physics2D.Raycast(borderLeft.transform.position, Vector2.down, distanceBorder);
            var borderRightRay = Physics2D.Raycast(borderRight.transform.position, Vector2.down, distanceBorder);

            var obstacleLeftRay = Physics2D.Raycast(obstacleLeft.transform.position, Vector2.left, distanceObstacle);
            var obstacleRightRay = Physics2D.Raycast(obstacleRight.transform.position, Vector2.right, distanceObstacle);

            

            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 5)
            {
                stateManager.SwitchState(new ShootState(stateManager));
            }
            else
            {
                if (obstacleRightRay.collider)
                {
                    Debug.Log("Intento saltar");
                    stateManager.SwitchState(new JumpRightState(stateManager));
                }
                else
                {
                    if (player.transform.position.x > transform.position.x)
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
                            stateManager.SwitchState(new BeginState(stateManager));
                        }
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
                            stateManager.SwitchState(new BeginState(stateManager));
                        }
                    }
                }
            }
        }
    }
}