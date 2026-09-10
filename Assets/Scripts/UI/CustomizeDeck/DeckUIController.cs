using DeckBuilder.Cards;
using GameManagers;
using PlayerSystem;
using System;
using TMPro;
using UI.CustomizeDeck.Card;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CustomizeDeck
{
    // Controls the CustomizeDeckScene UI layout, card movement between AvailableCards and MasterDeck
    // and triggers stage progression
    public class DeckUIController : MonoBehaviour
    {
        [SerializeField] private Transform availableCardsContainer;
        [SerializeField] private Transform masterDeckContainer;

        [SerializeField] private GameObject availableCardPrefab;
        [SerializeField] private GameObject masterDeckCardPrefab;
        [SerializeField] private TMP_Text nextStageText;

        [SerializeField] private Button nextBattleButton;

        [SerializeField] private Transform rewardUIGO;

        private PlayerData playerData;

        #region Unity Lifecycle

        private void Start()
        {
            // Bind to GameManager's PlayerData
            if (GameManager.Instance != null)
            {
                playerData = GameManager.Instance.PlayerData;
            }

            // Setup button listener
            if (nextBattleButton != null)
            {
                nextBattleButton.onClick.AddListener(OnNextBattleClicked);
            }

            if (rewardUIGO != null)
                rewardUIGO.gameObject.SetActive(false);

            RefreshUI();
        }

        #endregion

        #region UI Rendering Logic

        // Clears and repopulates the UI grids based on updated PlayerData
        public void RefreshUI()
        {
            if (playerData == null)
                return;

            // Update Next Stage text label
            if (nextStageText != null)
            {
                nextStageText.text = playerData.GetCurrentStage().ToString();
            }

            // Clear old UI items
            ClearContainer(availableCardsContainer);
            ClearContainer(masterDeckContainer);

            // Populate Available Cards Store
            foreach (var card in playerData.AvailableCards)
            {
                TryCreateCardUI(card, availableCardsContainer, RemoveCard, MoveToMasterDeck, null);
            }

            // Populate Master Deck Store
            foreach (var card in playerData.MasterDeck)
            {
                TryCreateCardUI(card, masterDeckContainer, RemoveCard, null, MoveToAvailableCards);
            }
        }

        private bool TryCreateCardUI(
            CardData card,
            Transform container,
            Action<CardData, Transform> onClickRemove,
            Action<CardData> onClickAdd = null,
            Action<CardData> onClickMove = null)
        {
            if (availableCardPrefab == null || masterDeckCardPrefab == null || container == null)
                return false;

            // Find an empty slot in the container and instantiate the card UI prefab there
            foreach (Transform child in container)
            {
                if (child.childCount > 0)
                {
                    continue;
                }

                GameObject cardObj = null;

                // Available card
                if (container == availableCardsContainer)
                {
                    cardObj = Instantiate(availableCardPrefab, child, false);
                }
                // Master deck card
                else if (container == masterDeckContainer)
                {
                    cardObj = Instantiate(masterDeckCardPrefab, child, false);
                }

                // RectTransform reset
                if (cardObj != null)
                {
                    RectTransform rect = cardObj.GetComponentInChildren<RectTransform>();
                    if (rect != null)
                    {
                        rect.localPosition = Vector3.zero;
                        rect.localScale = Vector3.one;
                    }
                }

                DeckCardUIController cardUI = null;
                if (cardObj != null)
                {
                    cardUI = cardObj.GetComponent<DeckCardUIController>();
                }
                
                if (cardUI != null)
                {
                    // Available card
                    if (container == availableCardsContainer)
                    {
                        cardUI.Setup(card, onClickRemove, onClickAdd, null);
                        return true;
                    }

                    // Master deck card
                    else if (container == masterDeckContainer)
                    {
                        cardUI.Setup(card, onClickRemove, null, onClickMove);
                        return true;
                    }
                    
                    else
                    {
                        Debug.LogWarning("Unknown container for card UI: " + container.name);
                        continue;
                    }
                }
            }

            // There are no available slots in the container
            return false;
        }

        private void ClearContainer(Transform container)
        {
            if (container == null)
                return;

            foreach (Transform child in container)
            {
                if (child.childCount > 0)
                {
                    Destroy(child.GetChild(0).gameObject);
                }
            }
        }

        #endregion

        #region Card Transfer Handlers

        // Remove a card from AvailableCards or MasterDeck list
        private void RemoveCard(CardData card, Transform parentContainer)
        {
            // Check if the card is in AvailableCards
            if (parentContainer == availableCardsContainer)
            {
                if (playerData.AvailableCards.Contains(card))
                {
                    playerData.RemoveCardFromAvailableCards(card);
                    RefreshUI();
                }
            }

            // Check if the card is in MasterDeck
            else if (parentContainer == masterDeckContainer)
            {
                if (playerData.MasterDeck.Contains(card))
                {
                    playerData.RemoveCardFromMasterDeck(card);
                    RefreshUI();
                }
            }

            // If the parent container is neither, log a warning
            else
            {
                Debug.LogWarning("Unknown parent container for card removal: " + parentContainer.name);
            }
        }

        // Moves a card from AvailableCards list to MasterDeck list
        private void MoveToMasterDeck(CardData card)
        {
            if (playerData == null || card == null)
                return;

            if (playerData.RemoveCardFromAvailableCards(card))
            {
                playerData.AddCardToMasterDeck(card);
                RefreshUI();
            }
        }

        // Moves a card from MasterDeck list to AvailableCards list
        private void MoveToAvailableCards(CardData card)
        {
            if (playerData == null || card == null)
                return;

            if (playerData.MoveCardToAvailableCards(card))
            {
                RefreshUI();
            }
        }

        #endregion

        #region Navigation

        // Called when player click the Next Battle button
        private void OnNextBattleClicked()
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.LoadCurrentStage();
            }
        }

        #endregion
    }
}