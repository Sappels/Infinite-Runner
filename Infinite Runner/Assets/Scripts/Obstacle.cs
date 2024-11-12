using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;
    public bool isWave;
    private PlayerMovement player;
    public int currentPos;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (!isWave)
        {
            speed = GameManager.Instance.speed;
            transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.Self);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player was killed my " + gameObject.name.ToString());
            GameManager.Instance.PlayerDeath();
        }

        if (other.gameObject.CompareTag("ObstacleKiller"))
        {
            if (isWave) return;
            Destroy(this.gameObject);
            GameManager.Instance.allObstacles.Remove(this.gameObject);
        }
    }
}
