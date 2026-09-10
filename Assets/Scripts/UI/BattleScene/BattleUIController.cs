using BattleSystem;
using BattleSystem.Instance;
using System;
using UI.BattleScene.Card;
using UnityEngine;

namespace UI.BattleScene
{
    public class BattleUIController : MonoBehaviour
    {
        [SerializeField] private Transform handContainer;

        [SerializeField] private GameObject battleCardPrefab;

        [SerializeField] private Transform stageStatusGO;

        [SerializeField] private PlayerInstance playerInstance;

        #region Unity Lifecycle

        private void Start()
        {
            if (BattleManager.Instance != null)
            {
                playerInstance = BattleManager.Instance.PlayerInstance;
            }

            // Setup button listener
            // ....

            if (stageStatusGO != null)
                stageStatusGO.gameObject.SetActive(false);

            RefreshUI();
        }

        #endregion

        #region UI Rendering Logic

        // Clears and repopulates the hand UI based on updated PlayerInstance
        public void RefreshUI()
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

        #region Card Interaction Logic

        // Play the card and remove it from hand
        private void PlayCard(CardInstance card)
        {
            
        }

        #endregion
    }
}