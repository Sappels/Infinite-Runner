using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapRotation : MonoBehaviour
{
    public bool isRotating;
    public PlayerMovement player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    public IEnumerator RotateMapTowards(float z, float rotationSpeed)
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
        player.SetPosition();
    }
}
