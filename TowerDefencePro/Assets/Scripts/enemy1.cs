using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy1 : MonoBehaviour {

    private Transform[] positions;

    private int _index = 0;

    public float _speed = 10;

	// Use this for initialization
	void Start () {
        positions = WayPoints.positions;

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
}
