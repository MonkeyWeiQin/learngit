using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuildManager : MonoBehaviour {
    public TurretData lasserTurretData;
    public TurretData missileTurretData;
    public TurretData standardTurretData;
    public int Money = 1000;
    public Text MoneyText;
    public Animator moneyAnim;

    public GameObject upgradeCanvas;
    public Button buttonUpgrade;


    public void UpdateMoney(int money)
    {
        Money += money;
        MoneyText.text ="￥" + Money;
    }

    /// <summary>
    /// 当前选择的炮台（即将建造的炮台） 
    /// </summary>
    public TurretData _selectedTurretData  ;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && _selectedTurretData._turretPrefab != null)
        {
            if (!EventSystem.current.IsPointerOverGameObject())  
            {
                //建造炮台
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider = Physics.Raycast(ray,out hit, 500, LayerMask.GetMask("MapCube"));
                if (isCollider)
                {
                    MapCube mapCube = hit.collider.gameObject.GetComponent<MapCube>(); ;
                    if(mapCube.turretGo == null)
                    {
                        //可以创建
                        if(Money> _selectedTurretData._cost)
                        {
                            //开始建造
                            mapCube.BuildTurret(_selectedTurretData._turretPrefab);
                            UpdateMoney(-_selectedTurretData._cost);
                        }
                        else
                        {
                            //提示钱不够
                            moneyAnim.SetTrigger("flicter");
                        }

                    }
                    else
                    {
                        //不可以创建
                        ShowUpgradeUI(mapCube.transform.position, mapCube.IsUpgrade);
                    }
                }
            }
        }
    }


    public void OnLasserSelected(bool isOn)
    {
        if (isOn)
        {
            _selectedTurretData = lasserTurretData;
        }
    }

    public void OnMissileSelected(bool isOn)
    {
        if (isOn)
        {
            _selectedTurretData = missileTurretData;
        }
    }

    public void OnStandardSelected(bool isOn)
    {
        if (isOn)
        {
            _selectedTurretData = standardTurretData;
        }
    }


    void ShowUpgradeUI(Vector3 pos, bool isDisableUpgrade = false)
    {
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable = !isDisableUpgrade;
    }


    void HideUpgradeUI()
    {
        upgradeCanvas.SetActive(false);
    }


    public void OnUpgradeButtonDown()
    {
        //TODO
    }

    public void OnDestroyButtonDown()
    {
        //TODO
    }
}

