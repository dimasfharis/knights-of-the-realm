using System.Collections.Generic;
using DeckBuilder.Cards;
using CharacterSystem;

namespace PlayerSystem
{
    // Holds persistent run data for the player
    // Can also be instantiated per stage to store pre-defined enemy setup
    [System.Serializable]
    public class PlayerData
    {
        // Permanent Character Roster
        public List<CharacterData> Roster = new List<CharacterData>();
        private int RosterMaxAmount = 4;

        // Permanent Master Deck
        public List<CardData> MasterDeck = new List<CardData>();
        public List<CardData> AvailableCards = new List<CardData>();
        private int MasterDeckMaxAmount = 10;
        private int AvailableCardsMaxAmount = 12;

        // Stats
        private int MaxEnergy = 3;
        private int CurrentStage = 1;

        #region Initialization & Reset

        // Resets stats and clears collections to default state
        private void InitializeDefault()
        {
            CurrentStage = 1;
            MaxEnergy = 3;
            Roster.Clear();
            MasterDeck.Clear();
            AvailableCards.Clear();
        }

        public void InitializeNewRun(List<CharacterData> starterRoster, List<CardData> starterDeck)
        {
            InitializeDefault();

            if (starterRoster != null)
            {
                Roster.AddRange(starterRoster);
            }

            if (starterDeck != null)
            {
                MasterDeck.AddRange(starterDeck);
            }
        }

        #endregion

        #region Helper Methods

        public bool AddCharacter(CharacterData character)
        {
            if (character != null && !Roster.Contains(character) && Roster.Count < RosterMaxAmount)
            {
                Roster.Add(character);

                return true;
            }

            return false;
        }

        public bool AddCardToMasterDeck(CardData card)
        {
            if (card != null && MasterDeck.Count < MasterDeckMaxAmount)
            {
                MasterDeck.Add(card);

                return true;
            }

            return false;
        }

        public bool AddCardToAvailableCards(CardData card)
        {
            if (card != null && AvailableCards.Count < AvailableCardsMaxAmount)
            {
                AvailableCards.Add(card);

                return true;
            }

            return false;
        }

        public bool MoveCardToAvailableCards(CardData card)
        {
            if (!MasterDeck.Contains(card))
                return false;

            bool canMoveToAvailable = AddCardToAvailableCards(card);

            if (!canMoveToAvailable)
                return false;

            return RemoveCardFromMasterDeck(card);
        }

        public bool RemoveCardFromMasterDeck(CardData card)
        {
            return MasterDeck.Remove(card);
        }

        public bool RemoveCardFromAvailableCards(CardData card)
        {
            return AvailableCards.Remove(card);
        }

        public bool UpgradeCard(CardData card)
        {
            if (!MasterDeck.Contains(card))
                return false;

            // do upgrade logic

            return true;
        }

        #endregion

        #region Public API

        public int GetCurrentStage()
        {
            return CurrentStage;
        }

        public void IncreaseStage()
        {
            CurrentStage++;
        }

        public int GetMaxEnergy()
        {
            return MaxEnergy;
        }

        #endregion
    }
}