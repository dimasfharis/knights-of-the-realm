using CharacterSystem;
using DeckBuilder.Cards;
using PlayerSystem;
using StageManagers;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        private PlayerData playerData = new PlayerData();
        private PlayerData currentEnemyData = new PlayerData();

        [SerializeField] private DataInitialization dataInitialization;
        [SerializeField] private StageManager stageManager;

        // Database of all characters & cards
        [SerializeField] private List<CharacterData> characterDatabase = new List<CharacterData>();
        [SerializeField] private List<CardData> cardDatabase = new List<CardData>();

        // Game run history
        // Stores card play command history received from BattleManager after each stage
        private List<object> runHistoryList = new List<object>();

        // Read-Only properties access for external classes
        public PlayerData PlayerData => playerData;
        public PlayerData CurrentEnemyData => currentEnemyData;
        public StageManager StageManager => stageManager;
        public int CurrentStageIndex => playerData != null ? playerData.GetCurrentStage() : 1;
        public IReadOnlyList<CharacterData> CharacterDatabase => characterDatabase;
        public IReadOnlyList<CardData> CardDatabase => cardDatabase;

        private const string DEFAULT_BATTLE_SCENE_NAME = "BattleScene";

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

            StartNewRun(dataInitialization.GetPlayerData().Roster, dataInitialization.GetPlayerData().MasterDeck);
        }

        #endregion

        #region Game Run Flow Control

        public void StartNewRun(List<CharacterData> starterRoster, List<CardData> starterDeck)
        {
            playerData.InitializeNewRun(starterRoster, starterDeck);
            runHistoryList.Clear();
        }

        // Prepares enemy data for the current stage via StageManager and transition to Battle Scene
        public void LoadCurrentStage(string sceneName = DEFAULT_BATTLE_SCENE_NAME)
        {
            if (stageManager == null)
            {
                Debug.LogWarning("[GameManager] StageManager reference is not assigned");
                stageManager = FindFirstObjectByType<StageManager>();
            }

            if (stageManager != null)
            {
                // Generate pre-defined enemy PlayerData for current stage
                currentEnemyData = stageManager.GenerateEnemyDataForStage(playerData.GetCurrentStage());
            }

            // Trigger battle scene initialization
            SceneManager.LoadScene(sceneName);
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
            playerData.IncreaseStage();

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
            return characterDatabase.Find(c => c != null && c.CharacterName == characterName);
        }

        public CardData GetCardFromDatabase(CardName cardName)
        {
            return cardDatabase.Find(c => c != null && c.CardName == cardName);
        }

        #endregion

        #region Public Accessors

        public DataInitialization GetDataInitialization()
        {
            return dataInitialization;
        }

        #endregion
    }
}