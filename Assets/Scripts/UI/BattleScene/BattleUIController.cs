using BattleSystem;
using BattleSystem.Instance;
using System;
using UI.BattleScene.Card;
using UI.BattleScene.Character;
using UnityEngine;

namespace UI.BattleScene
{
    public class BattleUIController : MonoBehaviour
    {
        [SerializeField] private Transform playerCharacterContainer;
        [SerializeField] private Transform enemyCharacterContainer;
        [SerializeField] private Transform handContainer;

        [SerializeField] private GameObject battleCharacterPrefab;
        [SerializeField] private GameObject battleCardPrefab;

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

            // Setup button listener
            // ....

            if (stageStatusGO != null)
                stageStatusGO.gameObject.SetActive(false);

            RefreshHandCardUI();
            RefreshBattleCharacterUI();
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
            foreach (var card in playerInstance.GetCardInstances())
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

        #region Card Interaction Logic

        // Play the card and remove it from hand
        private void PlayCard(CardInstance card)
        {
            
        }

        private void SelectCharacter(CharacterInstance character)
        {

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