using PlayerSystem;
using System.Collections.Generic;
using UnityEngine;

public class DataInitialization : MonoBehaviour
{
    [SerializeField] private PlayerData playerData = new PlayerData();
    [SerializeField] private List<PlayerData> enemyDataList = new List<PlayerData>();

    [SerializeField] private Dictionary<int, PlayerData> enemyDataDictionary = new Dictionary<int, PlayerData>();

    #region Unity Lifecycle

    private void Awake()
    {
        InitializeEnemyDataDictionary();
    }

    #endregion

    #region Initialization

    private void InitializeEnemyDataDictionary()
    {
        enemyDataDictionary.Clear();
        for (int i = 0; i < enemyDataList.Count; i++)
        {
            enemyDataDictionary.Add(i + 1, enemyDataList[i]);
        }
    }

    #endregion

    #region Public Accessors

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public PlayerData GetEnemyDataForStage(int stageIndex)
    {
        if (enemyDataDictionary.TryGetValue(stageIndex, out PlayerData enemyData))
        {
            return enemyData;
        }
        else
        {
            Debug.LogWarning($"No enemy data found for stage index: {stageIndex}");
            return null;
        }
    }

    #endregion
}
