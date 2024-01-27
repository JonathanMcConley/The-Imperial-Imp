using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    private bool isHomingPlayer;
    private GameObject player;
    public float speed;
    public Rigidbody rb;
    private Vector3 direction;
    private Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        isHomingPlayer = false;
        player = GameObject.FindGameObjectWithTag("Player");
        manager = (Manager)FindObjectOfType(typeof(Manager));
    }

    // Update is called once per frame
    void Update()
    {
       if(isHomingPlayer) 
        {
            home();
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
        if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("Ball")); 
        {
            isHomingPlayer = true;
            Debug.Log("I'm homing you now");
        }
        if (collision.gameObject.CompareTag("Player")) 
        {
            manager.increaseScoreBy(50);
            Destroy(gameObject);
        }
    }

    private void home() 
    {
        direction = (player.transform.position-transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
