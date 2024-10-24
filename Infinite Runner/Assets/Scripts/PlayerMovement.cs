using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Road road;
    public int currentPos;

    private Rigidbody rb;

    private float timeOnCurrentRoad;         // Time spent on the current road
    private float requiredTimeOnRoad = 5f;   // Time required to set the bool to true

    private int currentRoadIndex = -1;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentPos = 4;
        transform.position = road.positions[currentPos].transform.position;
    }

    void Update()
    {
        Move();
        CheckCurrentRoad();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentPos == 0) return;
            currentPos--;
            SetPosition();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentPos == 8) return;
            currentPos++;
            SetPosition();
        }

        if (currentPos == 2)
        {
            StartCoroutine(road.mapRotation.RotateMapTowards(90, 200));
        }
        else if (currentPos == 3 || currentPos == 5)
        {
            StartCoroutine(road.mapRotation.RotateMapTowards(0, 200));
        }else if (currentPos == 6)
        {
            StartCoroutine(road.mapRotation.RotateMapTowards(-90, 200));
        }
    }

    public void SetPosition()
    {
        transform.position = road.positions[currentPos].transform.position;
    }
    private int GetRoadIndex(int position)
    {
        // Determine which road the player is on based on current position
        if (position >= 0 && position <= 2) // Road 1
        {
            return 1; // Indicate Road 1
        }
        else if (position >= 3 && position <= 5) // Road 2
        {
            return 2; // Indicate Road 2
        }
        else if (position >= 6 && position <= 8) // Road 3
        {
            return 3; // Indicate Road 3
        }
        else
        {
            return -1; // Invalid position
        }
    }

    private void CheckCurrentRoad()
    {
        // Determine the current road based on currentPos
        int roadIndex = GetRoadIndex(currentPos);

        // If the road index has changed, reset the timer and bools accordingly
        if (roadIndex != currentRoadIndex)
        {
            // Reset all road bools when changing roads
            ResetRoadBools();
            timeOnCurrentRoad = 0; // Reset timer when changing roads
            currentRoadIndex = roadIndex; // Update current road index
        }

        // If the player is on a valid road, increase the time spent on that road
        if (roadIndex != -1)
        {
            timeOnCurrentRoad += Time.deltaTime; // Increase time on current road

            // Check if the required time on the road has been met
            if (timeOnCurrentRoad >= requiredTimeOnRoad)
            {
                SetRoadBool(roadIndex, true); // Set the corresponding road bool to true
            }
        }
    }
    private void SetRoadBool(int roadIndex, bool value)
    {
        // Set the corresponding road bool to the specified value
        switch (roadIndex)
        {
            case 1:
                GameManager.Instance.isOnRoad1 = value;
                break;
            case 2:
                GameManager.Instance.isOnRoad2 = value;
                break;
            case 3:
                GameManager.Instance.isOnRoad3 = value;
                break;
        }
    }


    private void ResetRoadBools()
    {
        // Reset all road bools to false
        GameManager.Instance.isOnRoad1 = false;
        GameManager.Instance.isOnRoad2 = false;
        GameManager.Instance.isOnRoad3 = false;
    }

}
