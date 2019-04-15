using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {
    public Wave[] waves;

    public Transform START;

    public float waveRate = 0.3f;

    public static int CountEnemyAlive = 0;

    void Start()
    {
        StartCoroutine(SpawnerEnemy());
    }
	
    IEnumerator SpawnerEnemy()
    {
        foreach (var wave in waves)
        {
            for (int i = 0; i < wave.Count; i++)
            {
                GameObject.Instantiate(wave.enemyPrefab, START.position, Quaternion.identity);
                CountEnemyAlive++;
                if (i != wave.Count-1)
                    yield return new WaitForSeconds(wave.rate);
            }
            while (CountEnemyAlive > 0)
            {
                yield return 0;
            }
            yield return new WaitForSeconds(waveRate);
        }
    }
}
