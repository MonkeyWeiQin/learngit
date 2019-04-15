using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCube : MonoBehaviour {
    /// <summary>
    /// 保存cube身上的炮台
    /// </summary>
    [HideInInspector]
    public GameObject turretGo;

    public void BuildTurret(GameObject prefab)
    {
        turretGo = GameObject.Instantiate(prefab, transform.position, Quaternion.identity);
    }


	
}
