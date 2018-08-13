using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Cattle.Game
{
    public class GameController : BaseComponent
    {
        public GameObject playerPrefab;
        public GameObject aiPlayerPrefab;

        public int aiPlayers;

        [Header("UI")]
        public Text countdownText;
        public GameObject panel;
        public GameObject deathPanel;
        public Text killsText;
        public Text ingameKillsText;

        PlayerGameScript player;
        List<PlayerGameScript> enemies;

        public bool gameRunning = false;

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
            ingameKillsText.text = "Kills: " + player.kills;
            
            if (player.deaths > 0)
            {
                //countdownText.text = "You died!";
                //countdownText.GetComponent<Animator>().SetTrigger("GrowAndBounce");
                
                deathPanel.SetActive(true);
                killsText.text = "Kills: " + player.kills;
            }
        }

        public void Play()
        {
            panel.SetActive(false);
            gameRunning = true;
        }

        public void ResetGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}