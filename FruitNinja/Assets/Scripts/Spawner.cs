using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] fruitToSpawnPrefab;
    public GameObject bombPrefab;
    // Hold the different positions where to spawn
    public Transform[] spawnPlaces;
    public float minWait = 0.3f;
    public float maxWait = 1f;
    public float minForce = 10f;
    public float maxForce = 20f;
    public int chanceToSpawnBomb = 10;

    // Start is called before the first frame update
    void Start()
    {
        // StartCoroutine for IEnumerator method
        StartCoroutine(SpawnFruits());
    }

    private IEnumerator SpawnFruits()
    {
        while (true)
        {
            // wait some seconds and then spawn agian
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
            // tr use the position of one of the places randomly
            Transform tr = spawnPlaces[Random.Range(0, spawnPlaces.Length)];
            // A new gameobject will check if it's bomb or fruit
            GameObject go = null;
            float rnd = Random.Range(0, 100);

            if (rnd < chanceToSpawnBomb)
            {
                go = bombPrefab;
            }
            else
            {
                go = fruitToSpawnPrefab[Random.Range(0, fruitToSpawnPrefab.Length)];
            }

            // Create a new gameobject (fruit)
            GameObject fruit = Instantiate(go, tr.transform.position, tr.transform.rotation);
            // Push the fruits
            fruit.GetComponent<Rigidbody2D>().AddForce(
                tr.transform.up * Random.Range(minForce, maxForce), ForceMode2D.Impulse);
            // Destroy the fruit after 5 sec
            Destroy(fruit, 5f);
        }
    }
}
