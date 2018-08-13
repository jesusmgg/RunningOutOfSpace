using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Cattle.Game
{
    public class GameController : BaseComponent
    {
        public GameObject playerPrefab;
        public GameObject aiPlayerPrefab;

        public int aiPlayers;

        [Header("UI")]
        public Text spawnStateText;

        PlayerGameScript player;
        List<PlayerGameScript> enemies;

        void Awake()
        {
            enemies = new List<PlayerGameScript>();
            
            // Instantiate players
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponentInChildren<PlayerGameScript>();

            for (int i = 0; i < aiPlayers; i++)
            {
                enemies.Add(Instantiate(aiPlayerPrefab, new Vector3(1000.0f, 0, 0), Quaternion.identity).GetComponent<PlayerGameScript>());
            }
        }

        void Update()
        {
            if (player.deaths > 0)
            {
                spawnStateText.text = "You died!";
                spawnStateText.GetComponent<Animator>().SetTrigger("GrowAndBounce");
            }
        }
    }
}