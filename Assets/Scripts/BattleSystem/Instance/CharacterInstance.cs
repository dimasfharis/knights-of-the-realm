using CharacterSystem;
using System;
using UnityEngine;

namespace BattleSystem.Instance
{
    public class CharacterInstance
    {
        // Identity & Blueprint
        public CharacterData Data { get; private set; }
        public string InstanceID { get; private set; }
        public CharacterName CharacterName { get; private set; }
        public string Description { get; private set; }
        public GameObject CharacterImageGO { get; private set; }

        // Runtime battle attributes (can change within battle)
        public int BaseMaxHealth { get; private set; }
        public int CurrentHealth { get; private set; }
        public int BaseDamage { get; private set; }

        #region Initialization

        public CharacterInstance(CharacterData data)
        {
            Data = data ?? throw new ArgumentNullException(nameof(data));

            InstanceID = Guid.NewGuid().ToString(); // Unique ID per character in a battle
            CharacterName = data.CharacterName;
            Description = data.Description;
            CharacterImageGO = data.CharacterImageGO;

            BaseMaxHealth = data.BaseMaxHealth;
            CurrentHealth = BaseMaxHealth;
            BaseDamage = data.BaseDamage;
        }

        #endregion

        #region Cost Modifiers

        // Change temporary health
        public void ModifyHealth(int amount)
        {
            CurrentHealth = Math.Clamp(CurrentHealth + amount, 0, BaseMaxHealth);
        }

        #endregion
    }
}