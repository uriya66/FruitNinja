using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombExplosion : MonoBehaviour
{
    public GameObject bomb;
    // Power of the explosion
    public float power = 10.0f;
    // Radius the sphere of explosion
    public float radius = 5.0f;
    // Upforce lift it off the explodes
    public float upforce = 1.0f;
    public GameObject bigExplosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Invoke("Detonate", 0);
    }

    void Detonate()
    {
        Instantiate(bigExplosionPrefab, transform.position, transform.rotation);
        Vector3 explosionPosition = bomb.transform.position;
        // A bunch of colliders explosion position and radius
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, radius);
        // for each individuak collider 
        foreach (Collider hit in colliders)
        {
            // The hit collider GetComponent of the hit collider
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPosition, radius, upforce, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }
}
