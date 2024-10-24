using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Road : MonoBehaviour
{
    public MapRotation mapRotation;
    public List<GameObject> positions;

    private void Start()
    {
        mapRotation = GetComponentInParent<MapRotation>();
    }
}
