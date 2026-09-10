using BattleSystem;
using UnityEngine;

namespace StateSystem.States
{
    public class BattleEndState : IState
    {
        public void OnEnter(BattleManager battleManager)
        {
            // Initialize battle setup here
        }

        public void Tick(BattleManager battleManager)
        {
            // Handle any updates needed during the battle start state
        }

        public void OnExit(BattleManager battleManager)
        {
            // Cleanup or transition logic here
        }
    }
}