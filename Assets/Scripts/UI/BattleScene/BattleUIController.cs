using BattleSystem;
using BattleSystem.Instance;
using GameManagers;
using StateSystem.States;
using System;
using TMPro;
using UI.BattleScene.Card;
using UI.BattleScene.Character;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleScene
{
    public class BattleUIController : MonoBehaviour
    {
        [SerializeField] private Transform playerCharacterContainer;
        [SerializeField] private Transform enemyCharacterContainer;
        [SerializeField] private Transform handContainer;

        [SerializeField] private GameObject battleCharacterPrefab;
        [SerializeField] private GameObject battleCardPrefab;

        [SerializeField] private TMP_Text DrawPileAmountText;
        [SerializeField] private TMP_Text DiscardPileAmountText;
        [SerializeField] private TMP_Text EnergyAmountText;

        [SerializeField] private Button EndTurnButton;

        [SerializeField] private Transform stageStatusGO;

        private PlayerInstance playerInstance;
        private PlayerInstance enemyInstance;

        #region Unity Lifecycle

        private void Start()
        {
            if (BattleManager.Instance != null)
            {
                playerInstance = BattleManager.Instance.PlayerInstance;
                enemyInstance = BattleManager.Instance.EnemyInstance;
            }

            playerInstance.GetBattleCardModel().OnCardDrawn += OnCardDrawnUpdateUI;
            playerInstance.GetBattleCardModel().OnCardPlayed += OnCardPlayedUpdateUI;
            playerInstance.GetBattleCardModel().OnCardDiscarded += OnCardDiscardedUpdateUI;
            playerInstance.GetBattleCardModel().OnHandCleared += OnHandClearedUpdateUI;

            if (stageStatusGO != null)
                stageStatusGO.gameObject.SetActive(false);

            RefreshHandCardUI();
            RefreshBattleCharacterUI();
        }

        private void OnDestroy()
        {
            playerInstance.GetBattleCardModel().OnCardDrawn -= OnCardDrawnUpdateUI;
            playerInstance.GetBattleCardModel().OnCardPlayed -= OnCardPlayedUpdateUI;
            playerInstance.GetBattleCardModel().OnCardDiscarded -= OnCardDiscardedUpdateUI;
            playerInstance.GetBattleCardModel().OnHandCleared -= OnHandClearedUpdateUI;
        }

        #endregion

        #region Hand Card UI Rendering Logic

        // Clears and repopulates the hand UI based on updated PlayerInstance
        public void RefreshHandCardUI()
        {
            if (playerInstance == null)
                return;

            // Clear old UI items
            ClearContainer(handContainer);

            // Populate Cards in Hand
            foreach (var card in playerInstance.GetHandCards())
            {
                TryCreateBattleCardUI(card, handContainer, PlayCard);
            }
        }

        private bool TryCreateBattleCardUI(
            CardInstance card,
            Transform container,
            Action<CardInstance> onClickPlay)
        {
            if (card == null || container == null || battleCardPrefab == null)
            {
                Debug.LogWarning("Invalid parameters for creating BattleCard UI.");
                return false;
            }

            // Instantiate the BattleCard prefab
            foreach (Transform child in container)
            {
                if (child.childCount > 0)
                {
                    continue;
                }

                GameObject cardObj = Instantiate(battleCardPrefab, child);

                if (cardObj != null)
                {
                    // Get the rectTransform of Canvas
                    RectTransform rect = cardObj.transform.GetChild(0).GetComponent<RectTransform>();
                    if (rect != null)
                    {
                        rect.localPosition = Vector3.zero;
                        rect.localScale = Vector3.one;
                    }
                }

                InBattleCardUIController cardUI = null;
                if (cardObj != null)
                {
                    cardUI = cardObj.GetComponent<InBattleCardUIController>();
                    if (cardUI != null)
                    {
                        cardUI.Setup(card, onClickPlay);
                        return true;
                    }
                }
            }

            // There are no available slots in the container
            return false;
        }

        #endregion

        #region Battle Character UI Rendering Logic

        public void RefreshBattleCharacterUI()
        {
            if (playerInstance == null || enemyInstance == null)
                return;

            // Clear old UI items
            ClearContainer(playerCharacterContainer);
            ClearContainer(enemyCharacterContainer);

            // Populate Character in both player and enemy containers
            foreach (var character in playerInstance.GetCharacterInstances())
            {
                TryCreateBattleCharacterUI(character, playerCharacterContainer, SelectCharacter);
            }

            foreach (var character in enemyInstance.GetCharacterInstances())
            {
                TryCreateBattleCharacterUI(character, enemyCharacterContainer, SelectCharacter);
            }
        }

        private bool TryCreateBattleCharacterUI(
            CharacterInstance character,
            Transform container,
            Action<CharacterInstance> onClickSelect)
        {
            if (character == null || container == null || battleCharacterPrefab == null)
            {
                Debug.LogWarning("Invalid parameters for creating BattleCharacter UI.");
                return false;
            }

            // Instantiate the BattleCharacter prefab
            foreach (Transform child in container)
            {
                if (child.childCount > 0)
                {
                    continue;
                }

                GameObject characterObj = Instantiate(battleCharacterPrefab, child);

                if (characterObj != null)
                {
                    // Get the rectTransform of Canvas
                    RectTransform rect = characterObj.transform.GetChild(0).GetComponent<RectTransform>();
                    if (rect != null)
                    {
                        rect.localPosition = Vector3.zero;
                        rect.localScale = Vector3.one;
                    }
                }

                InBattleCharacterUIController characterUI = null;
                if (characterObj != null)
                {
                    characterUI = characterObj.GetComponent<InBattleCharacterUIController>();
                    if (characterUI != null)
                    {
                        characterUI.Setup(character, onClickSelect);
                        return true;
                    }
                }
            }

            // There are no available slots in the container
            return false;
        }

        #endregion

        #region Card and Energy UI Rendering Logic

        private void RefreshCardAndEnergyUI()
        {
            BattleCardModel battleCardModel = playerInstance.GetBattleCardModel();

            DrawPileAmountText.text = battleCardModel != null ? battleCardModel.DrawPile.Count.ToString() : "?";
            DiscardPileAmountText.text = battleCardModel != null ? battleCardModel.DiscardPile.Count.ToString().ToString() : "?";
            EnergyAmountText.text = $"{playerInstance.GetCurrentEnergy()}/{playerInstance.GetMaxEnergy()}";
        }

        #endregion

        #region Card Interaction Logic

        // Play the card and remove it from hand
        private void PlayCard(CardInstance card)
        {
            if (BattleManager.Instance.currentState is not PlayerTurnInputState)
            {
                Debug.Log("Not be in turn input to select card");
                return;
            }

            playerInstance.GetBattleCardModel().PlayCard(card);
        }

        private void SelectCharacter(CharacterInstance character)
        {

        }

        #endregion

        #region Card Interaction Event Listener

        private void OnCardDrawnUpdateUI(CardInstance cardInstance)
        {
            RefreshHandCardUI();
            RefreshCardAndEnergyUI();
        }

        private void OnCardPlayedUpdateUI(CardInstance cardInstance)
        {
            RefreshHandCardUI();
            RefreshCardAndEnergyUI();
        }

        private void OnCardDiscardedUpdateUI(CardInstance cardInstance)
        {
            RefreshHandCardUI();
            RefreshCardAndEnergyUI();
        }

        private void OnHandClearedUpdateUI()
        {
            RefreshHandCardUI();
            RefreshCardAndEnergyUI();
        }

        #endregion

        #region Helper

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
    }
}