using UnityEngine;

namespace Cattle.Game
{
    public class PlayerGameScript : BaseGameScript
    {
        public bool isHuman;
        
        public float respawnTime = 3.0f;
        public bool isAlive;

        int kills;
        float respawnTimer;

        void Start()
        {
            respawnTimer = respawnTime;
            isAlive = false;

            kills = 0;
        }

        void Update()
        {
            if (respawnTimer > 0.0f)
            {
                respawnTimer -= Time.deltaTime;
            }

            if (respawnTimer <= 0.0f)
            {
                if (!isAlive)
                {
                    Spawn();
                }
            }
        }

        void Spawn()
        {
            Debug.Log("Spawn");
            
            isAlive = true;
        }
    }
}