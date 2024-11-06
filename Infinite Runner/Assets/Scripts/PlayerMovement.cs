using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Road road;
    public CameraSwitcher switcher;
    private SnapToGround snap;
    public GameObject dustCloudFx;

    private Rigidbody rb;

    public int currentPos;
    public bool isRotating;

    private float timeOnCurrentRoad;         // Time spent on the current road
    private float requiredTimeOnRoad = 5f;   // Time required to set the bool to true

    private int currentRoadIndex = -1;

    public Animator playerAnimator;



    void Start()
    {
        ResetRoadBools();
        rb = GetComponent<Rigidbody>();
        snap = GetComponent<SnapToGround>();
        currentPos = 4;
        transform.position = new Vector3(road.positions[currentPos].transform.position.x, snap.SnapPlayerToGroundPosition().y, road.positions[currentPos].transform.position.z);
        Invoke("SwitchToSprintAnimation", 5f);
    }

    void Update()
    {
        if (!GameManager.Instance.isGamePaused)
        {
            Move();
            CheckCurrentRoad();
        }
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            if (currentPos == 0) return;
            currentPos--;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            if (currentPos == 8) return;
            currentPos++;
        }
            SetPosition();

        if (currentPos == 2)
        {
            StopAllCoroutines();
            switcher.SwitchCamera(0);
            StartCoroutine(RotatePlayerTowards(-45, 2000));
        }
        else if (currentPos == 3 || currentPos == 5)
        {
            StopAllCoroutines();
            switcher.SwitchCamera(1);
            StartCoroutine(RotatePlayerTowards(0, 2000));
        }else if (currentPos == 6)
        {
            StopAllCoroutines();
            switcher.SwitchCamera(2);
            StartCoroutine(RotatePlayerTowards(45, 2000));
        }
    }

    public void SetPosition()
    {
        transform.position = road.positions[currentPos].transform.position;
        //transform.position = new Vector3(road.positions[currentPos].transform.position.x, snap.SnapPlayerToGroundPosition().y, road.positions[currentPos].transform.position.z);
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

    private void SwitchToSprintAnimation()
    {
        playerAnimator.SetTrigger("timeToSprint");
        dustCloudFx.SetActive(true);
        GameManager.Instance.speed += 10f;
    }

    public IEnumerator RotatePlayerTowards(float z, float rotationSpeed)
    {
        isRotating = true;
        Quaternion targetRotation = Quaternion.Euler(0, 0, z);

        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            // Rotate towards target rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null; // Wait for the next frame
        }

        // Snap rotation to the target rotation to avoid overshooting
        transform.rotation = targetRotation;
        isRotating = false;
        SetPosition();
    }

}

