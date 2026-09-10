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
        // Runtime character instance
        private List<CharacterInstance> characterInstances = new List<CharacterInstance>();
        private int characterInstancesMaxAmount = 4;

        // Runtime card instance
        private BattleCardModel battleCardModel = new BattleCardModel();
        private int cardInstancesMaxAmount = 10;

        // Stats
        private int maxEnergy;
        private int currentEnergy;
        private int currentStage;

        #region Constructor

        public PlayerInstance(PlayerData playerData)
        {
            maxEnergy = playerData != null ? playerData.GetMaxEnergy() : 3;
            currentEnergy = maxEnergy;
            currentStage = playerData != null ? playerData.GetCurrentStage() : 1;

            CloneCardInstancesToBattleCardModel(playerData);
            CloneCharacterInstancesFromRoster(playerData);
        }

        #endregion

        #region Initialization

        private void CloneCardInstancesToBattleCardModel(PlayerData playerData)
        {
            if (playerData == null || playerData.MasterDeck.Count <= 0)
            {
                Debug.LogWarning("PlayerData is null or MasterDeck is empty. Cannot clone card instances.");
                return;
            }

            battleCardModel.InitializeBattleDeck(playerData.MasterDeck);
        }

        private void CloneCharacterInstancesFromRoster(PlayerData playerData)
        {
            if (playerData == null || playerData.Roster.Count <= 0)
            {
                Debug.LogWarning("PlayerData is null or Roster is empty. Cannot clone character instances.");
                return;
            }

            foreach (var character in playerData.Roster)
            {
                CharacterInstance characterInstance = new CharacterInstance(character);

                if (characterInstances.Count < characterInstancesMaxAmount)
                {
                    characterInstances.Add(characterInstance);
                }
                else
                {
                    Debug.LogWarning("Reached maximum character instances limit. Cannot add more characters.");
                    break;
                }
            }
        }

        #endregion

        #region Accessors

        public BattleCardModel GetBattleCardModel()
        {
            return battleCardModel;
        }

        public List<CharacterInstance> GetCharacterInstances()
        {
            return characterInstances;
        }

        public List<CardInstance> GetDrawPileCards()
        {
            return battleCardModel.DrawPile;
        }

        public List<CardInstance> GetHandCards()
        {
            return battleCardModel.Hand;
        }

        public List<CardInstance> GetDiscardPileCards()
        {
            return battleCardModel.DiscardPile;
        }

        public int GetCardInstancesMaxAmount()
        {
            return cardInstancesMaxAmount;
        }

        public int GetMaxEnergy()
        {
            return maxEnergy;
        }

        public int GetCurrentEnergy()
        {
            return currentEnergy;
        }

        public int GetCurrentStage()
        {
            return currentStage;
        }

        #endregion
    }
}