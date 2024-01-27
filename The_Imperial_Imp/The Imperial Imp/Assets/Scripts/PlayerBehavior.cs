using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    public float baseMovementSpeed;
    private float currentMovementSpeed;
    public Rigidbody rb;
    public float jumpHeight;
    private int jumps;
    // Start is called before the first frame update
    void Start()
    {
        currentMovementSpeed = baseMovementSpeed;
        jumps = 2;
    }

    // Update is called once per frame
    void Update()
    {
        //W moves forward, S moves backward
        verticalInput = Input.GetAxis("Vertical");
        rb.AddForce(verticalInput * Vector3.right * currentMovementSpeed, ForceMode.Force);
        if (rb.velocity.x > currentMovementSpeed)
        {
            rb.velocity = new Vector3(currentMovementSpeed, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.x < -currentMovementSpeed)
        {
            rb.velocity = new Vector3(-currentMovementSpeed, rb.velocity.y, rb.velocity.z);
        }
        if (verticalInput == 0.0f) 
        {
            rb.velocity = new Vector3(0.0f, rb.velocity.y, rb.velocity.z);
        }
        //A moves Left, D moves Right
        horizontalInput = Input.GetAxis("Horizontal");
        rb.AddForce(horizontalInput * Vector3.back * currentMovementSpeed, ForceMode.Force);
        if (rb.velocity.z > currentMovementSpeed) 
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, currentMovementSpeed);
        }
        if (rb.velocity.z < -currentMovementSpeed) 
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, -currentMovementSpeed);
        }
        if (horizontalInput == 0.0f)
        {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0.0f);
        }
        //Space to Jump
        if (Input.GetKeyDown(KeyCode.Space) && jumps > 0)
        {
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumps--;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) 
        {
            jumps = 2;
        }
    }
}
