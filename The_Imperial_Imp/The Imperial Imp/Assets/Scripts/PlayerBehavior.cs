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
    private bool hasJumped;
    public float coyoteTime;
    public GameObject throwingBall;
    public Vector3 ballSpawn;
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
            rb.velocity = new Vector3(rb.velocity.x, 0.0f, rb.velocity.z);
            rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
            jumps--;
            hasJumped = true;
        }
        //Shift to throw a ball
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            ballSpawn = new Vector3(transform.position.x + 1.3f, transform.position.y, transform.position.z);
            Instantiate(throwingBall, ballSpawn, Quaternion.identity);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")) 
        {
            hasJumped = false;
            jumps = 2;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor")&&!hasJumped) 
        {
            StartCoroutine("coyoteTimeCoroutine");
        }
    }

    IEnumerator coyoteTimeCoroutine() 
    {
        yield return new WaitForSeconds(coyoteTime);
        if (!hasJumped)
        {
            jumps--;
        }
    }
}
