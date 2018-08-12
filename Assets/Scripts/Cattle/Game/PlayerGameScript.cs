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
            Debug.Log(respawnTimer);
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
        }

        void Spawn()
        {
            List<BreakableTileGameObject> tileList = FindObjectsOfType<BreakableTileGameObject>().ToList();
            
            if (tileList.Count > 0)
            {
                BreakableTileGameObject tile = tileList[Random.Range(0, tileList.Count)];
                if (tile != null)
                {
                    Debug.Log("Spawn");
                    transform.position = new Vector3(tile.transform.position.x, spawnHeight, 0.0f);
                    isAlive = true;
                }
            }
        }
    }
}