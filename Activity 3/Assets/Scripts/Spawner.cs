using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] 
    private Transform spawn;

    [SerializeField] 
    private GameObject enemy;

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            Instantiate(enemy, spawn.position, Quaternion.identity);
            yield return new WaitForSeconds(9f);
        }

    }
}
