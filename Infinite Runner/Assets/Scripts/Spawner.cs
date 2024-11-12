using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    public bool canSpawn = true;

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
                onMyRoadForTooLong = false;
                break;
        }
    }

    public IEnumerator SpawnObstacle()
    {
        GameObject randomSpawner = spawners[Random.Range(0, spawners.Count)];
        GameObject obstacle;
        Obstacle obstacleScript;

        if (canSpawn)
        {
            //Spawn Obstacles
            obstacle = Instantiate(
                    obstacles[Random.Range(0, obstacles.Count)],
                    randomSpawner.transform.position,
                    randomSpawner.transform.rotation
                );
            obstacleScript = obstacle.GetComponent<Obstacle>();
            obstacleScript.currentPos = int.Parse(randomSpawner.name);
            obstacle.name = obstacle.name + randomSpawner.name;

            obstacle.transform.SetParent(road.transform);

            GameManager.Instance.allObstacles.Add(obstacle);

        }
        float spawnInterval = Mathf.Clamp(1f / (GameManager.Instance.speed * 0.035f), 0.5f, 1.5f);

        // Adding some randomness to the spawn rate for variation
        float randomAdjustment = Random.Range(-0.3f, 0.3f);
        spawnInterval += randomAdjustment;
        yield return new WaitForSeconds(spawnInterval);
        StartCoroutine(SpawnObstacle());
    }

}
