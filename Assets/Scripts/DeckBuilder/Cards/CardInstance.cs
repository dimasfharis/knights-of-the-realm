using System;

namespace DeckBuilder.Cards
{
    public class CardInstance
    {
        // Identity & Blueprint
        public string InstanceID { get; private set; }
        public CardData Data { get; private set; }
        public int Level { get; private set; }

        // Runtime battle attributes (can change within battle)
        public int CurrentCost { get; private set; }
        public bool IsBurnOnPlay { get; set; }

        // Constant Variable
        private const float BONUS_STATS_PER_LEVEL = 0.3f;

        #region Initialization

        public CardInstance(CardData data, int level = 1)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));
            InstanceID = Guid.NewGuid().ToString(); // Unique ID per card in a battle
            Level = level;

            IsBurnOnPlay = data.BurnOnPlay;
            ResetCost();
        }

        #endregion

        #region Cost Modifiers

        // Return cost to template default value
        public void ResetCost()
        {
            CurrentCost = Data.BaseCost;
        }

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