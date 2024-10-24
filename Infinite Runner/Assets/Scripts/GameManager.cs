using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<GameObject> allObstacles;
    public float speed = 10f;
    public float maxSpeed;
    public float accelerationRate;

    public bool isOnRoad1;
    public bool isOnRoad2;
    public bool isOnRoad3;

    public static GameManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        IncreaseSpeedOverTime();
    }

    private void IncreaseSpeedOverTime()
    {
        // Increment speed with acceleration rate
        speed += accelerationRate * Time.deltaTime;

        // Cap the speed to maxSpeed
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

}
