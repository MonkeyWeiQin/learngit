using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Turret : MonoBehaviour {
    public List<GameObject> enemys = new List<GameObject>();
    public GameObject upgradeCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            enemys.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemys.Remove(other.gameObject);
    }

    /// <summary>
    /// 1秒攻击一次
    /// </summary>
    public float _attackRateTime = 1;
    public float timer = 0;
    //子弹
    public GameObject bulletPrefab;
    //子弹发射点
    public Transform FirePosition;

    public Transform Head;

    private void Start()
    {
        timer = _attackRateTime;
        //GameObject.Instantiate(upgradeCanvas, this.transform);
        //Application.streamingAssetsPath
    }

    private void Update()
    {
        if (enemys.Count > 0 && enemys[0] != null)
        {
            Vector3 targetPosition = enemys[0].transform.position;
            targetPosition.y = Head.position.y;
            Head.LookAt(targetPosition);
        }
        timer += Time.deltaTime;
        if (timer >= _attackRateTime && enemys.Count > 0)
        {
            timer = 0;
            Attack();
        }
        
    }

    void Attack()
    {
        GameObject bullet = GameObject.Instantiate(bulletPrefab, FirePosition.position, Quaternion.identity);
        //找到目标
        /*
        Transform target = null;
        while (target==null)
        {
            for (int i = 0; i < enemys.Count; i++)
            {
                if (enemys[i] != null)
                {
                    target = enemys[i].transform;
                    break;
                }
            }
        }*/
        if (enemys[0] == null)
        {
            UpdateEnemy();
        }
        if (enemys.Count > 0)
        {
            bullet.GetComponent<Bullet>().SetTarget(enemys[0].transform);
            Destroy(bullet, 4);
        }
        else
        {
            timer = _attackRateTime;
        }
    }


    void UpdateEnemy()
    {
        //enemys.RemoveAll(null);
        List<int> emptyIndex = new List<int>();
        for (int i = 0; i < enemys.Count; i++)
        {
            if (enemys[i] == null)
            {
                emptyIndex.Add(i);
            }
        }

        for (int i = 0; i < emptyIndex.Count; i++)
        {
            enemys.RemoveAt(emptyIndex[i]-i);
        }
    }

    public void EnemyDieHandler(GameObject obj)
    {

    }
}
