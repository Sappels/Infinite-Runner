using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tsunami : MonoBehaviour
{
    private float speed;
    private Vector3 direction;
    private Vector3 startPosition;

    public Spawner spawner;

    public GameObject wave, water;
    public ParticleSystem waterFx;

    private bool canMove;

    private void Start()
    {
        startPosition = transform.position;
        direction = -Vector3.forward;
        StartCoroutine(Flood());
    }

    void Update()
    {
        if (canMove)
        {
            transform.Translate(direction * speed * Time.deltaTime, Space.Self);
        }

        if (transform.position.z < 335f)
        {
            Debug.Log("Flooded!");
            GameManager.Instance.areaFlooded = true;
        }
        else
        {
            Debug.Log("NotFlooded");
        }

    }

    private IEnumerator Flood()
    {
        transform.position = startPosition;
        yield return new WaitForSeconds(5f);
        wave.SetActive(true);
        water.SetActive(true);
        waterFx.Play();

        spawner.canSpawn = false;
        canMove = true;
        direction = -Vector3.forward;
        speed = GameManager.Instance.speed + 50f;

        yield return new WaitForSeconds(10f);

        speed = 1f;
        direction = Vector3.down;

        yield return new WaitForSeconds(1f);

        spawner.canSpawn = true;
        GameManager.Instance.areaFlooded = false;
        canMove = false;
        wave.SetActive(false);
        water.SetActive(false);
        waterFx.Stop();

        StartCoroutine(Flood());
    }

    public void Reset()
    {
        StopAllCoroutines();
        spawner.canSpawn = true;
        GameManager.Instance.areaFlooded = false;
        canMove = false;
        wave.SetActive(false);
        water.SetActive(false);
        waterFx.Stop();

        StartCoroutine(Flood());
    }
}
