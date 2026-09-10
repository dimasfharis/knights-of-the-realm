using BattleSystem;
using UnityEngine;

namespace StateSystem.States
{
    public class BattleStartState : IState
    {
        public void OnEnter(BattleManager battleManager)
        {
            battleManager.InstanceInitialization();
            BattleCardModel battleCardModel = battleManager.PlayerInstance.GetBattleCardModel();

            battleCardModel.DrawToHandSize();
            Debug.Log("Entering Battle Start State");

            battleManager.ChangeState(new PlayerTurnInputState());
        }

        public void Tick(BattleManager battleManager)
        {
            // Handle any updates needed during the battle start state
        }

        public void OnExit(BattleManager battleManager)
        {
            Debug.Log("Exiting Battle Start State");
            // Cleanup or transition logic here
        }
    }
}