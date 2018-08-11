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
        [SerializeField] private float distance;

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
            Debug.DrawRay(borderLeft.transform.position, Vector2.down, Color.cyan);
            Debug.DrawRay(borderRight.transform.position, Vector2.down, Color.cyan);
            Debug.DrawRay(obstacleLeft.transform.position, Vector2.left, Color.red);
            Debug.DrawRay(obstacleRight.transform.position, Vector2.right, Color.red);
        }

        private void Update()
        {
            var playerPosition = player.transform.position - transform.position;

            var borderLeftRay = Physics2D.Raycast(borderLeft.transform.position, Vector2.down, distance);
            var borderRightRay = Physics2D.Raycast(borderRight.transform.position, Vector2.down, distance);

            if (Mathf.Abs(player.transform.position.x - transform.position.x) < 5)
            {
                stateManager.SwitchState(new ShootState(stateManager));
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