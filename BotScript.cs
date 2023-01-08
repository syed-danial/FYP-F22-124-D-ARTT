using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    float speed = 31f; // moveSpeed
    //Animator animator;
    public Transform ball;
    public bool statue = false;
    public Transform aimTargetBot; // aiming gameObject
    float force = 4.5f;

    public Transform[] targets; // array of targets to aim at
    Vector3 targetPosition; // position to where the bot will want to move
    ShotManager shotManager; // shot manager class/component
    [SerializeField] Ball bal;
    Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        targetPosition.x = targetPosition.x ; // initialize the targetPosition to its initial position in the court   - 0.400f
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //y++;
        //if(statue)
        Move(); // calling the move method
        
        //else
            //targetPosition = transform.position;
    }

     Vector3 PickTarget() // picks a random target from the targets array to be aimed at
    {
        int randomValue = Random.Range(0, targets.Length); // get a random value from 0 to length of our targets array-1
        return targets[randomValue].position; // return the chosen target
    }

    Shot PickShot() // picks a random shot to be played
    {
        int randomValue = Random.Range(0, 2); // pick a random value 0 or 1 since we have 2 shots possible currently
        if (randomValue == 0) // if equals to 0 return a top spin shot type
            return shotManager.topSpin;
        else                   // else return a flat shot type
            return shotManager.flat;
    }

    void Move()
    {
        //if(statue == true)
        targetPosition.x = ball.position.x; // update the target position to the ball's x position so the bot only moves on the x axis
        // if(ball.position.y > -2.4 && ball.position.x >= -1420)
        //     targetPosition.y = ball.position.y; // update the target position to the ball's y position so the bot only moves on the y axis
        //targetPosition.y -=1;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // lerp it's position
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            Vector3 dir = PickTarget() - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir * (force / 2.5f);
            other.GetComponent<Rigidbody>().useGravity = true;
            statue = true;
            animator.Play("response");
            if(bal != null)
            { 
                bal.again = true;
                bal.botplay = true;
                Debug.Log("aa" + bal.botplay);
            }
            //Debug.Log(statue);
            //Debug.Log(targets.Length);
        }
    }
}
