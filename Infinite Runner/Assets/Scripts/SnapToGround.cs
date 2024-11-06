using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    public float rayDistance = 10f;         // Maximum distance for the ray to check for ground
    public LayerMask groundLayer;           // Layer for ground objects
    private float heightOffset;             // Calculated offset to align the model's base to the ground
    public MeshRenderer meshRenderer;

    public bool isObstacle;
    public bool needsOffset;

    void Start()
    {
        CalculateHeightOffset();

        if (isObstacle)
        {
            SnapObstacleToGroundPosition();
        }
    }

    void CalculateHeightOffset()
    {
        
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

    public Vector3 SnapPlayerToGroundPosition()
    {
        RaycastHit hit;
        Vector3 hitData = new Vector3();
        Vector3 rayOrigin = transform.position;

        // Cast a ray downwards from the current position
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance, groundLayer))
        {
            // Snap to the point of contact on the ground, subtracting the calculated offset
            
            //transform.position = hit.point + new Vector3(0, heightOffset, 0);
            hitData = hit.point + new Vector3(0, heightOffset, 0);
        } else 
        { 
            return new Vector3(); 
        }
        return hitData;
    }

    void SnapObstacleToGroundPosition()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;

        // Cast a ray downwards from the current position
        if (Physics.Raycast(rayOrigin, Vector3.down, out hit, rayDistance, groundLayer))
        {
            // Snap to the point of contact on the ground, subtracting the calculated offset
            if (needsOffset)
            {
                transform.position = hit.point + new Vector3(0, (heightOffset / 3), 0);
            }
            else
            {
                transform.position = hit.point;
            }
        }
    }

}


