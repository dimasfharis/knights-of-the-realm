using GameManagers;
using PlayerSystem;
using TMPro;
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

        [SerializeField] private GameObject deckCardPrefab;
        [SerializeField] private TMP_Text nextStageText;

        [SerializeField] private Button nextBattleButton;

        [SerializeField] private string battleSceneName = "BattleScene";

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
                nextStageText.text = playerData.CurrentStage.ToString();
            }

            // Clear old UI items
            ClearContainer(availableCardsContainer);
            ClearContainer(masterDeckContainer);

            // Populate Available Cards Store
            foreach (var card in playerData.AvailableCards)
            {
                TryCreateCardUI(card, availableCardsContainer)
            }
        }

        #endregion
    }
}