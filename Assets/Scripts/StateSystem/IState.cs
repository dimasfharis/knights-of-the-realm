using BattleSystem;
using UnityEngine;

namespace StateSystem
{
    public interface IState
    {
        void OnEnter(BattleManager battleManager);
        void Tick(BattleManager battleManager);
        void OnExit(BattleManager battleManager);
    }
}