using System;
using System.Collections.Generic;
using UnityEngine;
using DeckBuilder.Cards;

namespace PlayerSystem
{
    [System.Serializable]
    public class PlayerData
    {
        // Permanent Character Roster
        public List<CharacterData> Roster = new List<CharacterData>();

        // Permanent Master Deck
        public List<CardData> MasterDeck = new List<CardData>();

        // Stats
        public int MaxEnergy = 3;
        public int CurrentStage = 1;

        #region Helper

        public void InitializeDefault()
        {
            CurrentStage = 1;
            MaxEnergy = 3;
        }

        #endregion
    }
}