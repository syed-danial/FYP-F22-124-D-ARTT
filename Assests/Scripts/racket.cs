using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racket : MonoBehaviour
{
    public Transform aimTarget;
    float speed = 7f;
    float force = 7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        aimTarget.Translate(new Vector3(h, 0, 0) * speed * Time.deltaTime);
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force;
            other.GetComponent<Rigidbody>().useGravity = true;
            Debug.Log(speed);
        }
    }
}
