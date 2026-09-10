using BattleSystem.Instance;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleScene.Card
{
    // Controller for individual card elements inside the player's hand during battle
    public class InBattleCardUIController : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TMP_Text cardNameText;
        [SerializeField] private TMP_Text costText;
        [SerializeField] private TMP_Text levelText;
        [SerializeField] private TMP_Text descriptionText;
        [SerializeField] private Button playButton;

        public CardInstance CardInstance { get; private set; }
        private Action<CardInstance> onClickPlay;

        #region Setup and Initialization

        public void Setup(
            CardInstance cardInstance,
            Action<CardInstance> onClickPlayCallback)
        {
            CardInstance = cardInstance;
            onClickPlay = onClickPlayCallback;

            if (iconImage != null && cardInstance.CardIcon != null) iconImage.sprite = cardInstance.CardIcon;
            if (cardNameText != null) cardNameText.text = cardInstance.CardName.ToString();
            if (costText != null) costText.text = cardInstance.CurrentCost.ToString();
            if (levelText != null) levelText.text = cardInstance.Level.ToString();
            if (descriptionText != null) descriptionText.text = cardInstance.Description;

            if (playButton != null)
            {
                playButton.onClick.RemoveAllListeners();
                playButton.onClick.AddListener(() => onClickPlay?.Invoke(CardInstance));
                playButton.onClick.AddListener(() => Debug.Log($"Play button clicked for card: {CardInstance.CardName}"));
            }
        }

        #endregion
    }
}