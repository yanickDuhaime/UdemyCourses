using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [Range(0,50)]
    [SerializeField] int poolSize = 5;
    [Range(0.1f,30f)]
    [SerializeField] float enemySpawnSpeed = 1f;

    GameObject[] pool;

    void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];
        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab,transform);
            pool[i].SetActive(false);
        }
    }
    
    void EnableObjectInPool(){
        
        foreach (var enemy in pool)
        {
            if (!enemy.activeInHierarchy)
            {
                enemy.SetActive(true);
                return;
            }
        }
    }

    private IEnumerator EnemySpawn()
    {
        while(true){
            EnableObjectInPool();
            yield return new WaitForSeconds(enemySpawnSpeed);
        }
    }
}
