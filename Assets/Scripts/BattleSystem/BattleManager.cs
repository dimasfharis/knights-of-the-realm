using BattleSystem.Instance;
using CommandSystem;
using GameManagers;
using UnityEngine;
using System.Collections.Generic;
using StateSystem;
using StateSystem.States;

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

        public List<Command> CommandQueue { get; private set; } = new List<Command>();

        public IState currentState { get; private set; }

        #region Unity Lifecycle

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            ChangeState(new BattleStartState());
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            currentState?.Tick(this);
        }

        #endregion

        #region Initialization

        public void InstanceInitialization()
        {
            // Initialize player and enemy instances
            PlayerInstance = new PlayerInstance(GameManager.Instance.PlayerData);
            EnemyInstance = new PlayerInstance(GameManager.Instance.CurrentEnemyData);
        }

        #endregion

        #region State Management

        public void ChangeState(IState newState)
        {
            currentState?.OnExit(this);
            currentState = newState;
            currentState?.OnEnter(this);
        }

        #endregion
    }
}