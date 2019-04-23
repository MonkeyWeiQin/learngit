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

    private Animator _upgradeCanvasAnimator;


    public void UpdateMoney(int money)
    {
        Money += money;
        MoneyText.text ="￥" + Money;
    }

    /// <summary>
    /// 当前选择的炮台（即将建造的炮台） 
    /// </summary>
    private TurretData _selectedTurretData  ;

    /// <summary>
    /// 场景中的游戏物体
    /// </summary>
    private MapCube _selectedMapCube;


    private void Start()
    {
        _upgradeCanvasAnimator = upgradeCanvas.GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) )
        {
            if (!EventSystem.current.IsPointerOverGameObject() && _selectedTurretData._turretPrefab != null)  
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
                            mapCube.BuildTurret(_selectedTurretData);
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
                        if(mapCube == _selectedMapCube && upgradeCanvas.activeInHierarchy)
                        {
                            StartCoroutine(HideUpgradeUI());
                        }
                        else
                        {
                            ShowUpgradeUI(mapCube.transform.position, mapCube.IsUpgrade);
                        }
                        _selectedMapCube = mapCube;
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
        StopCoroutine("HideUpgradeUI");
        upgradeCanvas.SetActive(false);
        upgradeCanvas.SetActive(true);
        upgradeCanvas.transform.position = pos;
        buttonUpgrade.interactable       = !isDisableUpgrade;
    }


    IEnumerator HideUpgradeUI()
    {
        _upgradeCanvasAnimator.SetTrigger("Hide");
        yield return new WaitForSeconds(0.5f);
        upgradeCanvas.SetActive(false);
    }


    public void OnUpgradeButtonDown()
    {
        if (Money >= _selectedMapCube._turretData._costUpgrade)
        {
            UpdateMoney(-_selectedMapCube._turretData._costUpgrade);
            //TODO
            //检测是否可以升级
            _selectedMapCube.UpgradeTurret();
        }
        else
        {
            moneyAnim.SetTrigger("flicter");
        }
        StartCoroutine(HideUpgradeUI());
    }

    public void OnDestroyButtonDown()
    {
        //TODO
        _selectedMapCube.DestroyTurret();
        StartCoroutine(HideUpgradeUI());
    }
}

