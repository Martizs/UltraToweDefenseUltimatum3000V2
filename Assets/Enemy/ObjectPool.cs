using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;

    [SerializeField]
    [Range(1, 100)]
    int poolSize = 5;

    [SerializeField]
    [Range(1f, 30f)]
    float spawnTimer = 3f;

    GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab);
            pool[i].transform.parent = transform;
            pool[i].SetActive(false);
        }
    }

    // void SpawnEnemy()
    // {
    //     Instantiate(enemyPrefab);
    // }

    void EnableObjectInPool()
    {
        foreach (GameObject enemyInPool in pool)
        {
            if (!enemyInPool.activeInHierarchy)
            {
                enemyInPool.SetActive(true);
                return;
            }
        }
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
