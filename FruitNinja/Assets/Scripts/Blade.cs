using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    public float minVelocity = 0.1f;
    private Vector3 lastMousePosition;
    private Vector3 mouseVelocity;
    private Collider2D collider;
    private Rigidbody2D rb;

    // Awake is called before the firt frame update and then also before all of the start methods were called
     void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        // Circle Collider of the blade
        collider = GetComponent<Collider2D>();
    }

    // FixedUpdate is called a fixed amount of time
    void FixedUpdate()
    {
        // Enabled the collider on base is mouse moving 
        collider.enabled = IsMouseMoving();
        SetBladeToMouse();
    }

    private void SetBladeToMouse()
    {
        // Get the mouse position in pixel coordinates
        var mousePosition = Input.mousePosition;
        // Because our camera is -10 z coordinate
        mousePosition.z = 10;
        // Set the position through the rigidbody of the blade
        // On the first main camera
        // ScreenToWorldPoint allow us to make a connection between the screen point to the world point
        rb.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    // For mobile when the user not touch the screen
    private bool IsMouseMoving()
    {
        Vector3 currentMousePositoin = transform.position;
        // How long this vector
        // magnitude return the length of this vector3
        float traveled = (lastMousePosition - currentMousePositoin).magnitude;

        lastMousePosition = currentMousePositoin;
        if(traveled > minVelocity)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
