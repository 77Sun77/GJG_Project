using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] Enemies;
    public GameObject Boss;

    public List<GameObject> EnemyList = new List<GameObject>();
    public float SpawnCooltime;

    public int SpawnCount;

    public IEnumerator coroutian;

    

    void Start()
    {
        AddList(0);
        coroutian = EnemySpawn();
        StartCoroutine(coroutian);
        

    }

    void Update()
    {
        
    }


    public void AddList(int count)
    {
        EnemyList.Add(Enemies[count]);
    }

    public bool IsTargetVisible(Camera _camera, Transform _transform)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        var point = _transform.position;
        foreach(var plane in planes)
        {
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        }
        return true;
    }

    IEnumerator EnemySpawn()
    {
        for(int i=0; i < 5; i++)
        {
            float Spawn_X = Random.Range(-15f, 15f);
            float Spawn_Y = Random.Range(-15f, 15f);
            Vector2 vec = new Vector2(Spawn_X, Spawn_Y);
            Transform tr = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], vec, Quaternion.identity).transform;
            SpawnCount++;
            spawn(tr);
            while (IsTargetVisible(Camera.main, tr))
            {
                spawn(tr);
                yield return null;
            }
            tr.gameObject.SetActive(true);
        }
        while (true)
        {
            yield return new WaitForSeconds(SpawnCooltime);
            while (SpawnCount > 50) yield return null;
            float Spawn_X = Random.Range(-15f, 15f);
            float Spawn_Y = Random.Range(-15f, 15f);
            Vector2 vec = new Vector2(Spawn_X, Spawn_Y);
            Transform tr = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], vec, Quaternion.identity).transform;
            SpawnCount++;
            spawn(tr);
            while (IsTargetVisible(Camera.main, tr))
            {
                spawn(tr);
                yield return null;
            }
            tr.gameObject.SetActive(true);
        }
        
    }

    public void SpawnBoss()
    {
        StopCoroutine(coroutian);
        //StartCoroutine(SpawnBoss_Co());
        
    }

    IEnumerator SpawnBoss_Co()
    {
        Transform tr = Instantiate(Boss).transform;
        spawn(tr);
        while (IsTargetVisible(Camera.main, tr))
        {
            spawn(tr);
            yield return null;
        }
        tr.gameObject.SetActive(true);
    }

    void spawn(Transform tr)
    {
        
        float Spawn_X = Random.Range(-15f, 15f);
        float Spawn_Y = Random.Range(-15f, 15f);
        Vector2 vec = new Vector2(Spawn_X, Spawn_Y);
        tr.position = vec;
    }
}
