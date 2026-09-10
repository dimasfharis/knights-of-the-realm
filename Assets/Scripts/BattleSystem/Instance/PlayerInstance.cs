using UnityEngine;
using PlayerSystem;
using System.Collections.Generic;

namespace BattleSystem.Instance
{
    /// <summary>
    /// Represents the player's instance in the battle system
    /// Holds player-specific data and state for the current battle
    /// </summary>
    public class PlayerInstance
    {
        // Runtime card instance
        private List<CardInstance> cardInstances = new List<CardInstance>();
        private int cardInstancesMaxAmount = 10;

        // Stats
        private int maxEnergy;
        private int currentStage;

        #region Constructor

        public PlayerInstance(PlayerData playerData)
        {
            maxEnergy = playerData != null ? playerData.GetMaxEnergy() : 3;
            currentStage = playerData != null ? playerData.GetCurrentStage() : 1;

            CloneCardInstancesFromMasterDeck(playerData);
            // Clone character ....
        }

        #endregion

        #region Initialization

        private void CloneCardInstancesFromMasterDeck(PlayerData playerData)
        {
            if (playerData == null || playerData.MasterDeck.Count <= 0)
            {
                Debug.LogWarning("PlayerData is null or MasterDeck is empty. Cannot clone card instances.");
                return;
            }

            foreach (var card in playerData.MasterDeck)
            {
                CardInstance cardInstance = new CardInstance(card);

                if (cardInstances.Count < cardInstancesMaxAmount)
                {
                    cardInstances.Add(cardInstance);
                }
                else
                {
                    Debug.LogWarning("Reached maximum card instances limit. Cannot add more cards.");
                    break;
                }
            }
        }

        #endregion

        #region Accessors

        public List<CardInstance> GetCardInstances()
        {
            return cardInstances;
        }

        public int GetCardInstancesMaxAmount()
        {
            return cardInstancesMaxAmount;
        }

        public int GetMaxEnergy()
        {
            return maxEnergy;
        }

        public int GetCurrentStage()
        {
            return currentStage;
        }

        #endregion
    }
}