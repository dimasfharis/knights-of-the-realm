using PlayerSystem;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameManagers;

namespace StageManagers
{
    public class StageManager : MonoBehaviour
    {
        public PlayerData GenerateEnemyDataForStage(int stage)
        {
            DataInitialization dataInitialization = GameManager.Instance.GetDataInitialization();

            PlayerData enemyPlayerData = dataInitialization.GetEnemyDataForStage(stage);

            return enemyPlayerData;
        }
    }
}