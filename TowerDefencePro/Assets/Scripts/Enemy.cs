using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {

    public float HP = 150;
    private float totalHP;

    private Transform[] positions;

    private int _index = 0;

    public float _speed = 10;

    public GameObject ExplotionEffect;

    private Slider HpBar;

	// Use this for initialization
	void Start () {
        positions   = WayPoints.positions;
        totalHP     = HP;
        HpBar       = GetComponentInChildren<Slider>();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
    }

    void Move()
    {
        if (_index > positions.Length - 1) return;
        transform.Translate((positions[_index].position - transform.position).normalized*Time.deltaTime* _speed);
        if (Vector3.Distance(positions[_index].position, transform.position) <= 0.2f)
        {
            _index++;
        }
        if (_index > positions.Length - 1)
        {
            ReachDestination();
        }
    }


    void ReachDestination()
    {
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        EnemySpawner.CountEnemyAlive--;
    }


    public void TakeDamage(float damageValue)
    {
        if (HP < 0) return;
        HP -= damageValue;
        UpdateHpBar(HP);
        if (HP <= 0)
        {
            GameObject explotion = GameObject.Instantiate(ExplotionEffect, transform.position, transform.rotation);
            Destroy(explotion,1);
            Destroy(this.gameObject);
        }
    }

    void UpdateHpBar(float hp)
    {
        HpBar.value = hp / totalHP;
    }
}
