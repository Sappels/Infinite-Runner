using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private List<GameObject> spawners;
    [SerializeField] private List<GameObject> obstacles;

    [SerializeField] private GameObject road;

    [SerializeField] private int roadIndex;

    private bool onMyRoadForTooLong;

    private void Start()
    {
        StartCoroutine(SpawnObstacle());
    }

    private void Update()
    {
        switch (roadIndex)
        {
            case 1:
                onMyRoadForTooLong = GameManager.Instance.isOnRoad1;
                break;
            case 2:
                onMyRoadForTooLong = GameManager.Instance.isOnRoad2;
                break;
            case 3:
                onMyRoadForTooLong = GameManager.Instance.isOnRoad3;
                break;
            default:
                break;
        }
    }

    public IEnumerator SpawnObstacle()
    {
        GameObject randomSpawner = spawners[Random.Range(0, spawners.Count)];
        GameObject obstacle;
        if (!onMyRoadForTooLong)
        {
            obstacle = Instantiate(
                obstacles[Random.Range(0, obstacles.Count)],
                randomSpawner.transform.position,
                randomSpawner.transform.rotation
            );
        }
        else
        {
             obstacle = Instantiate(
                wall,
                spawners[1].transform.position,
                spawners[1].transform.rotation
            );
        }

        obstacle.transform.SetParent(road.transform);

        GameManager.Instance.allObstacles.Add(obstacle);
        yield return new WaitForSeconds(Random.Range(0.5f, 1.0f));
        StartCoroutine(SpawnObstacle());
    }

}
