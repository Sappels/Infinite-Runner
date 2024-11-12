using UnityEngine;

public class SnapToGround : MonoBehaviour
{
    public float rayDistance = 10f;         // Maximum distance for the ray to check for ground
    public LayerMask groundLayer;           // Layer for ground objects
    private float heightOffset;             // Calculated offset to align the model's base to the ground
    public MeshRenderer meshRenderer;
    public Transform meshTransform;

    public bool isObstacle;
    public bool needsOffset;

    private bool snap;

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
        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.down), out hit, rayDistance, groundLayer))
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

    public void UpdatePlayerYPosition()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;

        // Cast a ray downwards from the player's current position
        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.down), out hit, rayDistance, groundLayer))
        {
            // Calculate the new Y position based on the ground's height, with heightOffset if necessary
            Vector3 targetPosition = transform.position;
            targetPosition.y = hit.point.y - 0.1f;

            // Update only the Y position to keep player above the ground
            meshTransform.position = targetPosition;
        }
    }


    void SnapObstacleToGroundPosition()
    {
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;

        // Cast a ray downwards from the current position
        if (Physics.Raycast(rayOrigin, transform.TransformDirection(Vector3.down), out hit, rayDistance, groundLayer))
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


