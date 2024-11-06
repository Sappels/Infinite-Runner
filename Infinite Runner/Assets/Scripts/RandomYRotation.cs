using UnityEngine;
using UnityEngine.Animations;

public class RandomYRotation : MonoBehaviour
{
    void Start()
    {
        // Generate a random X rotation between 0 and 360 degrees
        float randomY = Random.Range(0f, 360f);

        // Create a rotation with a random X and Y/Z set to zero
        Quaternion randomRotation = Quaternion.Euler(transform.rotation.x, randomY, transform.rotation.z);

        transform.localRotation = randomRotation;
    }
}
