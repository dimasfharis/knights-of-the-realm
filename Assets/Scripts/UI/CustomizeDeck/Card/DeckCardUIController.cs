using System;
using DeckBuilder.Cards;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CustomizeDeck.Card
{
    // Controller for individual card elements inside AvailableCards or MasterDeck grid
    public class DeckCardUIController : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text cardNameText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button addButton;
        [SerializeField] private Button removeButton;
        [SerializeField] private Button moveButton;

        public CardData CardData { get; private set; }
        private Action<CardData> onClickAdd;
        private Action<CardData> onClickRemove;
        private Action<CardData> onClickMove;

        #region Public Methods

        public void Setup(CardData cardData, Action<CardData> onClickAddCallback, Action<CardData> onClickRemoveCallback, Action<CardData> onClickMoveCallback)
        {
            CardData = cardData;
            onClickAdd = onClickAddCallback;
            onClickRemove = onClickRemoveCallback;
            onClickMove = onClickMoveCallback;

            if (iconImage != null && cardData.CardIcon != null) iconImage.sprite = cardData.CardIcon;
            if (cardNameText != null) cardNameText.text = cardData.CardName.ToString();
            if (costText != null) costText.text = cardData.BaseCost.ToString();
            if (levelText != null) levelText.text = cardData.Level.ToString();
            if (descriptionText != null) descriptionText.text = cardData.Description;

            if (addButton != null)
            {
                addButton.onClick.RemoveAllListeners();
                addButton.onClick.AddListener(() => onClickAdd?.Invoke(CardData));
            }

            if (removeButton != null)
            {
                removeButton.onClick.RemoveAllListeners();
                removeButton.onClick.AddListener(() => onClickRemove?.Invoke(CardData));
            }

            if (moveButton != null)
            {
                moveButton.onClick.RemoveAllListeners();
                moveButton.onClick.AddListener(() => onClickMove?.Invoke(CardData));
            }
        }

        #endregion
    }
}