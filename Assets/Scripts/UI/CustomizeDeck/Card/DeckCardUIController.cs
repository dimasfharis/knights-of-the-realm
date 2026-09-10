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
        private Action<CardData, Transform> onClickRemove;
        private Action<CardData> onClickMove;

        #region Setup and Initialization

        public void Setup(
            CardData cardData,
            Action<CardData, Transform> onClickRemoveCallback,
            Action<CardData> onClickAddCallback = null,
            Action<CardData> onClickMoveCallback = null)
        {
            CardData = cardData;
            onClickRemove = onClickRemoveCallback;
            onClickAdd = onClickAddCallback;
            onClickMove = onClickMoveCallback;

            if (iconImage != null && cardData.CardIcon != null) iconImage.sprite = cardData.CardIcon;
            if (cardNameText != null) cardNameText.text = cardData.CardName.ToString();
            if (costText != null) costText.text = cardData.BaseCost.ToString();
            if (levelText != null) levelText.text = cardData.Level.ToString();
            if (descriptionText != null) descriptionText.text = cardData.Description;

            if (removeButton != null)
            {
                removeButton.onClick.RemoveAllListeners();
                removeButton.onClick.AddListener(() => onClickRemove?.Invoke(CardData, transform.parent.parent));
                removeButton.onClick.AddListener(() => Debug.Log($"Remove button clicked for card: {CardData.CardName}"));
            }

            if (addButton != null)
            {
                addButton.onClick.RemoveAllListeners();
                addButton.onClick.AddListener(() => onClickAdd?.Invoke(CardData));
                addButton.onClick.AddListener(() => Debug.Log($"Add button clicked for card: {CardData.CardName}"));
            }

            if (moveButton != null)
            {
                moveButton.onClick.RemoveAllListeners();
                moveButton.onClick.AddListener(() => onClickMove?.Invoke(CardData));
                moveButton.onClick.AddListener(() => Debug.Log($"Move button clicked for card: {CardData.CardName}"));
            }
        }

        #endregion
    }
}