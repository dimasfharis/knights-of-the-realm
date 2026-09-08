using System.Collections.Generic;
using CharacterSystem;
using DeckBuilder.Cards;
using PlayerSystem;
using StageManagers;
using UnityEngine;

namespace GameManagers
{
    /// <summary>
    /// Central manager that orchestrates the entire game run
    /// Holds persistent player data, current stage's enemy data, master databases
    /// and receives battle decision history upon stage completion
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField] private PlayerData playerData = new PlayerData();

        [SerializeField] private PlayerData currentEnemyData;

        [SerializeField] private StageManager stagemanager;

        // Database of all characters & cards
        [SerializeField] private List<CharacterData> characterDatabase = new List<CharacterData>();
        [SerializeField] private List<CardData> cardDatabase = new List<CardData>();

        // Game run history
        // Stores card play command history received from BattleManager after each stage
        private List<object> runHistoryList = new List<object>();

        // Read-Only properties access for external classes
        public PlayerData PlayerData => playerData;
        public PlayerData CurrentEnemyData => currentEnemyData;
        public StageManager StageManager => stagemanager;
        public int CurrentStageIndex => playerData != null ? playerData.CurrentStage : 1;
        public IReadOnlyList<CharacterData> CharacterDatabase => characterDatabase;
        public IReadOnlyList<CardData> CardDatabase => cardDatabase;

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        #endregion

        #region Game Run Flow Control

        public void StartNewRun(List<CharacterData> starterRoster, List<CardData> starterDeck)
        {
            playerData.InitializeNewRun(starterRoster, starterDeck);
            runHistoryList.Clear();

            LoadCurrentStage();
        }

        private void LoadCurrentStage()
        {
            if (stagemanager == null)
            {
                Debug.LogError("[GameManager] StageManager reference is not assigned");
                return;
            }

            // Generate pre-defined enemy PlayerData for current stage
            currentEnemyData = stagemanager.GenerateEnemyDataForStage(playerData.CurrentStage);

            // Trigger battle scene initialization
            // stageManager.StartStageBattle(playerData, currentEnemyData)
            // ....
        }

        // Callback called by BattleManager when a stage is successfully completed
        // Receives battle decision history and handles stage progression
        public void OnStageCompleted(object battleHistory)
        {
            if (battleHistory != null)
            {
                runHistoryList.Add(battleHistory);
            }

            // Progress to next stage
            playerData.CurrentStage++;

            // Notify StageUI or transition system
            // ....
        }

        // Callback called by BattleManager when the player loses the stage
        public void OnStageFailed()
        {
            // Trigger popup information of Win/Lose
            // ....
        }

        #endregion

        #region Database Lookups

        public CharacterData GetCharacterFromDatabase(CharacterName characterName)
        {
            return characterDatabase.Find(c => c != null && c.characterName == characterName);
        }

        public CardData GetCardFromDatabase(CardName cardName)
        {
            return cardDatabase.Find(c => c != null && c.CardName == cardName);
        }

        #endregion
    }
}