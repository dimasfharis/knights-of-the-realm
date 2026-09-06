using UnityEngine;

namespace DeckBuilder.Cards
{
    [CreateAssetMenu(fileName = "NewCardData", menuName = "DeckBuilder/Cards/Card Data")]
    public class CardData : ScriptableObject
    {
        // Identity & Visuals
        [Header("Identity & Visuals")]
        [SerializeField] private string cardID;
        [SerializeField] private string cardName;
        [TextArea(2, 4)]
        [SerializeField] private string description;
        [SerializeField] private Sprite cardIcon;

        public string CardID => cardID;
        public string CardName => cardName;
        public string Description => description;
        public Sprite CardIcon => cardIcon;

        // Card Mechanics & Stats
        [Header("Mechanics & Stats")]
        [SerializeField] private CardType cardType;
        [SerializeField] private TargetType targetType;
        [SerializeField] private int baseCost = 1;
        [SerializeField] private int baseValue = 5; // Damage / Shield / Healing Amount
        [SerializeField] private bool burnOnPlay = false;

        public CardType CardType => cardType;
        public TargetType TargetType => targetType;
        public int BaseCost => baseCost;
        public int BaseValue => baseValue;
        public bool BurnOnPlay => burnOnPlay;
    }
}