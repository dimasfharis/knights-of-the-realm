using DeckBuilder.Cards;
using UnityEngine;

namespace CharacterSystem
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable Objects/Character Data")]
    public class CharacterData : ScriptableObject
    {
        // Identity & Visuals
        [Header("Identity & Visuals")]
        [SerializeField] private string characterID;
        [SerializeField] private CharacterName characterName;
        [TextArea(2, 4)]
        [SerializeField] private string description;
        [SerializeField] private GameObject characterImageGO;

        public string CharacterID => characterID;
        public CharacterName CharacterName => characterName;
        public string Description => description;
        public GameObject CharacterImageGO => characterImageGO;

        // Base Stats
        [Header("Base Stats")]
        [SerializeField] private int baseMaxHealth = 50;
        [SerializeField] private int baseDamage = 5;

        public int BaseMaxHealth => baseMaxHealth;
        public int BaseDamage => baseDamage;
        
    }
}