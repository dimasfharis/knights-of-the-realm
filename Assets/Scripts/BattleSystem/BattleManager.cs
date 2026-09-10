using BattleSystem.Instance;
using GameManagers;
using UnityEngine;

namespace BattleSystem
{
    /// <summary>
    /// Central manager that orchestrates the battle system
    /// Holds current battle state, player and enemy data, and manages turn-based logic
    /// </summary>
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        public PlayerInstance PlayerInstance { get; private set; }
        public PlayerInstance EnemyInstance { get; private set; }

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            InstanceInitialization();
        }

        #endregion

        #region Initialization

        private void InstanceInitialization()
        {
            // Initialize player and enemy instances
            PlayerInstance = new PlayerInstance(GameManager.Instance.PlayerData);
            EnemyInstance = new PlayerInstance(GameManager.Instance.CurrentEnemyData);
        }

        #endregion
    }
}