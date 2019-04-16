using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 子弹的飞行管理
/// </summary>
public class Bullet : MonoBehaviour {

    public int damage = 50;

    public float _speed = 40;

    private Transform target;

    public float distanceArriveTarget = 1.2f;


    public GameObject explosionEffectPrefab;
	
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Die();
            return;
        }
        if (target != null)
        {
            transform.LookAt(target.position);
            transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            Vector3 dir = target.position - transform.position;
            if (dir.magnitude <= distanceArriveTarget)
            {
                target.GetComponent<Enemy>().TakeDamage(damage);
                Die();
            }
        }
    }


    void Die()
    {
        GameObject explotionEffect = GameObject.Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
        Destroy(this.gameObject);
        Destroy(explotionEffect, 1);
    }
}
