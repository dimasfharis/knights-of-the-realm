using CharacterSystem;
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

        private int currentHealth;
        private int maxHealth;

        public CharacterData CharacterData { get; private set; }

        private bool isSelectedAsTarget = false;
        private Action<InBattleCharacterUIController> onCharacterClicked;

        #region Setup and Initialization

        public void Setup(CharacterData characterData, Action<InBattleCharacterUIController> onClickCallback)
        {
            CharacterData = characterData;
            onCharacterClicked = onClickCallback;
        }

        #endregion
    }
}