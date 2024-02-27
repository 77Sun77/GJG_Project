using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Enemies;

    public List<GameObject> EnemyList = new List<GameObject>();
    public float SpawnCooltime;
    
    void Start()
    {
        Invoke("EnemySpawn", SpawnCooltime);
        AddList(0);
    }

    void Update()
    {
        
    }

    void EnemySpawn()
    {
        Instantiate(EnemyList[Random.Range(0, EnemyList.Count)]);
        Invoke("EnemySpawn", SpawnCooltime);
    }

    public void AddList(int count)
    {
        EnemyList.Add(Enemies[count]);
    }
}
