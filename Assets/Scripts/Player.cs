using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float minX; // minimum x
    public float maxX; // maximum x
    public float minZ; // minimum y
    public float maxZ; // maximum y

    public float speed;
    public float maxTiltAngle = 30.0f;
    private Vector2 movementInput;
    private Rigidbody rb;

    public GameObject bolts;
    public float fireRate = 0.5f; // Shoot rate
    private float nextFireTime = 0.0f;

    private void Start()
    {
        minX = -Screen.width / 200;
        maxX = Screen.width / 200;
        minZ = 0;
        maxZ = Screen.height / 100;
        speed = 5.0f;
        rb = GetComponent<Rigidbody>();
    }
    public void OnMove(InputValue input)
    {
        movementInput = input.Get<Vector2>(); // Get input

    }

    private void FixedUpdate()
    {
        // switch to vector3
        Vector3 movement = new Vector3(movementInput.x, 0.0f, movementInput.y);

        // Normalize
        movement.Normalize();
        
        // Movement
        MoveShip(transform.position + movement * speed * Time.fixedDeltaTime);
        
        // Restrict position
        RestrictPosition();

        // Fire
        Shooting();
    }

    void MoveShip(Vector3 newPosition)
    {
        // Using speed to count angle
        float tiltAngle = Mathf.Lerp(0, maxTiltAngle, Mathf.Abs(movementInput.x * speed * 0.1f));

        tiltAngle *= Mathf.Sign(movementInput.x);

        rb.rotation = Quaternion.Euler(0, 0, -tiltAngle);
        
        rb.position = newPosition;
    }

    void RestrictPosition()
    {
        // CurrentPosition
        Vector3 newPosition = transform.position;

        // Restrict current position x
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);

        // Restrict current position y
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // Update transform
        transform.position = newPosition;
    }

    void Shooting()
    {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time > nextFireTime)
        {
            Transform shootPoint = transform.Find("Shoot");

            // Instantiate prefab and set bullet to world coordinate
            Instantiate(bolts, shootPoint).transform.SetParent(null);

            nextFireTime = Time.time + fireRate;
        }
    }
}
