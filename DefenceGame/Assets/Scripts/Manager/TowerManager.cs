using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    // 타워 전용 풀
    public List<TowerData> towerPool;

    public Transform towerParent;

    public void RandSpawnTower(Vector2 spawnPos)
    {
        if (GameManager.Instance.Cost())
        {

        }
    }
}
