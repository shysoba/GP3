using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    [SerializeField] private GameObject item;
    void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {   
            Instantiate(item, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(2f);
        }
        
    }
}
