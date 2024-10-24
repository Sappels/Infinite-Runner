using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        speed = GameManager.Instance.speed;
    }

    private void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ouch!");
        }
    }
}
