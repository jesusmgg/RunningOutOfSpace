using System.Collections.Generic;
using System.Linq;
using Cattle.Tiles;
using UnityEngine;

namespace Cattle.Game
{
    public class PlayerGameScript : BaseGameScript
    {
        public bool isHuman;

        public float spawnHeight = 22.0f;
        public float deathHeight = -13.0f;
        public float respawnTime = 3.0f;
        public bool isAlive;

        public int kills;
        public int deaths;
        
        float respawnTimer;

        void Start()
        {
            respawnTimer = respawnTime;
            isAlive = false;

            kills = 0;
            deaths = 0;
        }

        void Update()
        {
            if (!isAlive)
            {
                if (respawnTimer > 0.0f)
                {
                    respawnTimer -= Time.deltaTime;
                }
                else if (respawnTimer <= 0.0f)
                {
                    Spawn();
                }
            }
            else
            {
                if (transform.position.y <= deathHeight || transform.position.y > spawnHeight + 10.0f)
                {
                    Die();
                }
            }
        }

        void Die()
        {
            deaths++;
            respawnTimer = respawnTime;
            isAlive = false;
        }

        void Spawn()
        {
            List<BreakableTileGameObject> tileList = FindObjectsOfType<BreakableTileGameObject>().ToList();
            
            if (tileList.Count > 0)
            {
                BreakableTileGameObject tile = tileList[Random.Range(0, tileList.Count)];
                if (tile != null)
                {
                    transform.position = new Vector3(tile.transform.position.x, spawnHeight, 0.0f);
                    isAlive = true;
                }
            }
        }
    }
}