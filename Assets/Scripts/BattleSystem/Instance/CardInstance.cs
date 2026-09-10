using DeckBuilder.Cards;
using System;
using UnityEngine;

namespace BattleSystem.Instance
{
    public class CardInstance
    {
        // Identity & Blueprint
        public CardData Data { get; private set; }
        public string InstanceID { get; private set; }
        public CardName CardName { get; private set; }
        public int Level { get; private set; }
        public string Description { get; private set; }
        public Sprite CardIcon { get; private set; }


        // Runtime battle attributes (can change within battle)
        public CardType CardType { get; private set; }
        public TargetType TargetType { get; private set; }
        public int CurrentCost { get; private set; }
        public int CurrentValue { get { return GetCalculatedValue(); } }
        public bool IsBurnOnPlay { get; private set; }

        // Constant Variable
        private const float BONUS_STATS_PER_LEVEL = 0.3f;

        #region Initialization

        public CardInstance(CardData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));

            InstanceID = Guid.NewGuid().ToString(); // Unique ID per card in a battle
            CardName = data.CardName;
            Level = data.Level;
            Description = data.Description;
            CardIcon = data.CardIcon;

            CardType = data.CardType;
            TargetType = data.TargetType;
            CurrentCost = data.BaseCost;
            IsBurnOnPlay = data.BurnOnPlay;
        }

        #endregion

        #region Cost Modifiers

        // Change temporary cost
        public void ModifyCost(int amount)
        {
            CurrentCost = Math.Max(0, CurrentCost + amount);
        }

        // Change cost to specific value
        public void SetCost(int fixedCost)
        {
            CurrentCost = Math.Max(0, fixedCost);
        }

        #endregion

        #region Value Calculation Logic

        // Calculate value of card (ex. damage/shield) based on card's level
        public int GetCalculatedValue()
        {
            int bonusPerLevel = UnityEngine.Mathf.RoundToInt(Data.BaseValue * BONUS_STATS_PER_LEVEL);
            return Data.BaseValue + ((Level - 1) * bonusPerLevel);
        }

        #endregion
    }
}