using BattleSystem.Instance;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI.BattleScene.Character
{
    public class InBattleCharacterUIController : MonoBehaviour
    {
        [SerializeField] private Image highlightOverlay;
        [SerializeField] private Image healthFillImage;
        [SerializeField] private GameObject characterVisual;
        [SerializeField] private Button selectButton;

        private int maxHealth;
        private int currentHealth;

        public CharacterInstance CharacterInstance { get; private set; }
        private Action<CharacterInstance> onClickSelect;

        private bool isSelectedAsTarget = false;

        #region Setup and Initialization

        public void Setup(
            CharacterInstance characterInstance,
            Action<CharacterInstance> onClickCallback)
        {
            CharacterInstance = characterInstance;
            onClickSelect = onClickCallback;

            // Setup the character visual representation
            if (characterVisual != null && characterInstance.CharacterImageGO != null)
            {
                GameObject characterGO = Instantiate(characterInstance.CharacterImageGO, characterVisual.transform, false);
                if (characterGO != null)
                {
                    // Get the rectTransform of Canvas
                    RectTransform rect = characterGO.transform.GetChild(0).GetComponent<RectTransform>();
                    if (rect != null)
                    {
                        rect.localScale = Vector3.one;
                    }
                }
            }

            maxHealth = characterInstance.BaseMaxHealth;
            currentHealth = characterInstance.CurrentHealth;

            if (selectButton != null)
            {
                selectButton.onClick.RemoveAllListeners();
                selectButton.onClick.AddListener(() => onClickSelect?.Invoke(CharacterInstance));
                selectButton.onClick.AddListener(() => Debug.Log($"Select button clicked for character: {CharacterInstance.CharacterName}"));
            }
        }

        #endregion
    }
}