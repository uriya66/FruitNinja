using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    public GameObject sliceFruitPrefab;
    public float explosionRadius = 5f;
    // Define how many points per fruit 
    private GameManager myGameManager;
    public int scoreAmount = 2;


    private void Start()
    {
        myGameManager = FindObjectOfType<GameManager>();
    }

    public void CreateSlicedFruit()
    {
        // Instantiate - Create a new gameobject at given position
        GameObject instance = Instantiate(sliceFruitPrefab, transform.position, transform.rotation);
        // Add explosion in the middle of 2 pieces with get the rigidbody of to pieces
        Rigidbody[] rbOnSliced = instance.transform.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rbOnSliced)
        {
            // Give the rigidbody a random rotation (rotate the fruit slice)
            rigidbody.transform.rotation = Random.rotation;
            // Add an explosion in center of the fruit
            rigidbody.AddExplosionForce(Random.Range(500, 1000), transform.position, explosionRadius);
        }

        // Increase the score
        FindObjectOfType<GameManager>().IncreaseScore(scoreAmount);

        // Play the sounds
        myGameManager.PlayRandomSound();
        // Destroy the current sliced fruit
        Destroy(instance, 5f);
        // Destroy the current fruit
        Destroy(gameObject);
    }

    // Destroy fruit is touched by the blade
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check the collosion item if it has a blade component 
        // If it has a blade component then this stored onside of b value
        // And if it doesn't b will be null it
        Blade b = collision.GetComponent<Blade>();
        // if null return
        if (!b)
        {
            return;
        }
        CreateSlicedFruit();
    }
}
