using UnityEngine;

public class TerrainScroller : MonoBehaviour
{
    public GameObject terrain1; // Assign the first terrain segment
    public GameObject terrain2; // Assign the second terrain segment
    public GameObject terrain3; // Assign the third terrain segment
    public float scrollSpeed; // Adjust the speed as desired

    private void Update()
    {
        scrollSpeed = GameManager.Instance.speed;
        // Move all three terrain segments forward along the z-axis
        terrain1.transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);
        terrain2.transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);
        terrain3.transform.Translate(Vector3.back * scrollSpeed * Time.deltaTime, Space.World);

        // Check each terrain's position relative to the player and reposition if behind
        RepositionTerrain(terrain1);
        RepositionTerrain(terrain2);
        RepositionTerrain(terrain3);
    }

    private void RepositionTerrain(GameObject terrainToMove)
    {
        // Calculate the maximum z position of the existing terrains
        if (terrainToMove.transform.localPosition.z <= -395f) { 

            // Position the terrainToMove just behind the furthest segment by repositionDistance
            terrainToMove.transform.localPosition = new Vector3(
                terrainToMove.transform.localPosition.x,
                terrainToMove.transform.localPosition.y,
                790f // Positioning behind the highest terrain
            );
        }
    }
}
