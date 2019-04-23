using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MapCube : MonoBehaviour {
    /// <summary>
    /// 保存cube身上的炮台
    /// </summary>
    [HideInInspector]
    public GameObject turretGo;
    /// <summary>
    /// 建造炮台时的特效
    /// </summary>
    public GameObject _buildEffect;
    private Renderer renderer;
    [HideInInspector]
    public bool IsUpgrade = false;
    [HideInInspector]
    public TurretData _turretData;


    private void Start()
    {
        renderer = this.GetComponent<Renderer>();
    }

    public void BuildTurret(TurretData turretData)
    {
        this._turretData    = turretData;
        IsUpgrade           = false;
        turretGo            = GameObject.Instantiate(turretData._turretPrefab, transform.position, Quaternion.identity);
        GameObject effect   = GameObject.Instantiate(_buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1);
    }


    private void OnMouseEnter()
    {
        if (turretGo == null && !EventSystem.current.IsPointerOverGameObject())
        {
            renderer.material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        renderer.material.color = Color.white;
    }



    public void UpgradeTurret()
    {
        if (IsUpgrade) return;
        Destroy(turretGo);
        IsUpgrade = true;
        turretGo  = GameObject.Instantiate(_turretData._turretUpgradePrefab, transform.position, Quaternion.identity);
        
    }


    public void DestroyTurret()
    {
        GameObject effect = GameObject.Instantiate(_buildEffect, transform.position, Quaternion.identity);
        Destroy(effect, 1.5f);
        Destroy(turretGo);
        IsUpgrade   = false;
        turretGo    = null;
        _turretData = null;
    }

}
