using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        speed = GameManager.Instance.speed;
        transform.Translate(-Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Ouch!");
            GameManager.Instance.youDiedMenu.SetActive(true);
            GameManager.Instance.SaveHighScore();
            GameManager.Instance.LoadHighScore();
            GameManager.Instance.StopOrStartGame();
        }

        if (other.gameObject.CompareTag("ObstacleKiller"))
        {
            Destroy(this.gameObject);
            GameManager.Instance.allObstacles.Remove(this.gameObject);
        }
    }
}
