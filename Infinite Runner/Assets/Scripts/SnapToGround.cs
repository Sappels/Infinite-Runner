using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    public float rayDistance = 10f;         // Maximum distance for the ray to check for ground
    public LayerMask groundLayer;           // Layer for ground objects
    private float heightOffset;             // Calculated offset to align the model's base to the ground

    void Start()
    {
        CalculateHeightOffset();
    }

    void Update()
    {
        SnapToGroundPosition();
    }

    void CalculateHeightOffset()
    {
        // Get the MeshRenderer component and calculate the bottom offset
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        if (meshRenderer != null)
        {
            // Calculate offset based on the half of the bounds' size in the Y axis
            heightOffset = meshRenderer.bounds.extents.y;
        }
        else
        {
            Debug.LogWarning("MeshRenderer not found on the object. Please ensure the model has a MeshRenderer component.");
        }
    }

    void SnapToGroundPosition()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;

        // Cast a ray downwards from the current position
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance, groundLayer))
        {
            // Snap to the point of contact on the ground, subtracting the calculated offset
            transform.position = hit.point + new Vector3(0, heightOffset, 0);
        }
    }

}


