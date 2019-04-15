using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 保存每一波敌人的生成所需要的属性
/// </summary>
//可序列化显示在Inspector
[System.Serializable]
public class Wave  {
    public GameObject enemyPrefab;
    public int Count;
    public float rate;
}
