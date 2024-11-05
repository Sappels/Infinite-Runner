using UnityEngine;

public class RandomXRotation : MonoBehaviour
{
    void Start()
    {
        // Generate a random X rotation between 0 and 360 degrees
        float randomX = Random.Range(0f, 360f);

        // Create a rotation with a random X and Y/Z set to zero
        Quaternion randomRotation = Quaternion.Euler(randomX, 0f, 0f);

        // Apply the rotation to the GameObject's transform
        transform.rotation = randomRotation;
    }
}
