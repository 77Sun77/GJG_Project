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
        StartCoroutine(EnemySpawn());
        AddList(0);

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
        while (true)
        {
            yield return new WaitForSeconds(SpawnCooltime);
            float Spawn_X = Random.Range(-15f, 15f);
            float Spawn_Y = Random.Range(-15f, 15f);
            Vector2 vec = new Vector2(Spawn_X, Spawn_Y);
            Transform tr = Instantiate(EnemyList[Random.Range(0, EnemyList.Count)], vec, Quaternion.identity).transform;
            while(IsTargetVisible(Camera.main, tr))
            {
                Spawn_X = Random.Range(-15f, 15f);
                Spawn_Y = Random.Range(-15f, 15f);
                vec = new Vector2(Spawn_X, Spawn_Y);
                tr.position = vec;
                yield return null;
            }
        }
        
    }
}
