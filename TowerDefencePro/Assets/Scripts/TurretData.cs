using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 炮台数据类
/// </summary>
[System.Serializable]
public class TurretData  {
    /// <summary>
    /// 炮台预制件
    /// </summary>
    public GameObject _turretPrefab;
    /// <summary>
    /// 炮台消耗金币
    /// </summary>
    public int _cost;
    /// <summary>
    /// 升级之后的预制件
    /// </summary>
    public GameObject _turretUpgradePrefab;
    /// <summary>
    /// 升级所需要的金币数
    /// </summary>
    public int _costUpgrade;
	
}

/// <summary>
/// 炮台的类型
/// </summary>
public enum TurretType
{
    LaserTurret,
    MissileTurret,
    StandardTurret,
}
