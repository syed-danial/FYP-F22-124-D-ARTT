using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    // public Vector3 initialImpulse;
    private Rigidbody myRb;
    Vector3 initialPos;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.useGravity = false;
        initialPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKeyDown("f"))
        // {
        //     myRb.useGravity = true;
        // }

        // Debug.Log(transform.position.y);

        if (transform.position.y < -5) {
            transform.position = initialPos;
            myRb.useGravity = false;
            myRb.velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "Paddle_one")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            myRb.useGravity = true;
        }
        
    }

    // public IEnumerator GravityDisableRoutine()
    // {
    //     myRb.useGravity = false;
    //     yield return new WaitForSeconds(10); //You may change this number of seconds
    //     myRb.useGravity = true;
    // }
}
