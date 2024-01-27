using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private bool isHomingPlayer;
    private bool hasTurnedAround;
    public Transform player;
    public float speed;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        isHomingPlayer = false;
        hasTurnedAround = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHomingPlayer) 
        {
            if (!hasTurnedAround)
            {
                rb.AddForce(Vector3.MoveTowards(transform.position, player.transform.position, speed), ForceMode.Acceleration);
                if (rb.velocity.magnitude > speed)
                {
                    rb.velocity = rb.velocity.normalized * speed;
                }
            }
            else
            {
                transform.position = new Vector3 (transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
                hasTurnedAround = true;
            }
        }
        else 
        {
            rb.AddForce(Vector3.right * speed, ForceMode.Force);
            if (rb.velocity.x > speed) 
            {
                rb.velocity = new Vector3(speed, rb.velocity.y, rb.velocity.z);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall")) 
        {
            isHomingPlayer = true;
            Debug.Log("I'm homing you now");
        }
        if (collision.gameObject.CompareTag("Player")) 
        {
            Destroy(gameObject);
        }
    }
}
