using BattleSystem;
using StateSystem;
using UnityEngine;

namespace StateSystem.States
{
    public class EnemyTurnExecutionState : IState
    {
        public void OnEnter(BattleManager battleManager)
        {
            // Initialize enemy turn execution here
        }

        public void Tick(BattleManager battleManager)
        {
            // Handle any updates needed during the enemy turn execution
        }

        public void OnExit(BattleManager battleManager)
        {
            // Cleanup or transition logic here
        }
    }
}