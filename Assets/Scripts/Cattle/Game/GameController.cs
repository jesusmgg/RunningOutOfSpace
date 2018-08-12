using UnityEngine;

namespace Cattle.Game
{
    public class GameController : BaseComponent
    {
        public GameObject PlayerPrefab;
        public GameObject AIPlayerPrefab;

        public int AIPlayers;

        void Start()
        {
            // Instantiate players
            Instantiate(PlayerPrefab, null);

            for (int i = 0; i < AIPlayers; i++)
            {
                Instantiate(AIPlayerPrefab, null);
            }
        }
    }
}