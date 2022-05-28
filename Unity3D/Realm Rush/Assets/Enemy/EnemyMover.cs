using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Enemy))]
public class EnemyMover : MonoBehaviour
{

    [SerializeField] List<Tile> path = new List<Tile>();

    [SerializeField] [Range(0f,5f)]float speed = 1f;

    Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    void OnEnable()
    {
        FindPath();
        ReturnToStart();
        StartCoroutine(FollowPath());
       
    }

    void FindPath()
    {
        path.Clear();
        GameObject parent = GameObject.FindGameObjectWithTag("Path");
        foreach (Transform child in parent.transform )
        {
            Tile tile = child.GetComponent<Tile>();
            if(tile != null)
                path.Add(tile);
        }
    }

    void ReturnToStart()
    {
        transform.position = path[0].transform.position;
    }

    IEnumerator FollowPath()
    {
        foreach (Tile waypoint in path)
        {
            Vector3 startPos = transform.position;
            Vector3 endPos = waypoint.transform.position;
            float travelPercent = 0f;
            transform.LookAt(endPos);
            while (travelPercent < 1)
            {
                travelPercent += speed * Time.deltaTime ;
                transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
                yield return new WaitForEndOfFrame();
            }
        }

        FinishPath();
    }

    private void FinishPath()
    {
        enemy.StealGold();
        gameObject.SetActive(false);
    }
}
