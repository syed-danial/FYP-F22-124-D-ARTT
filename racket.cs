using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class racket : MonoBehaviour
{
    
    [SerializeField] BotScript bot;
    [SerializeField] Ball bal;
    public Transform aimTarget;
    float speed = 21f;
    float force = 4.5f;
    public Transform ball;
    Vector3 targetPosition;
    Vector3 targetPos;
    float temp = 0.0f;
    bool now_move = false;
    Animator animator;
    //[SerializeField] TextMeshProUGUI NetHit;

    // [SerializeField] transform serveRight;
    // [SerializeField] transform serveLeft;

    bool serveright = true;
    //bool serveleft;

    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        targetPos = transform.position;
        targetPos.x = transform.position.x;
        targetPos.y = transform.position.y;
        targetPos.z = transform.position.z;

        animator = GetComponent<Animator>();
        //NetHit.text = " ";
        //bot = Botplayer.GetComponent<BotScript>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        
            

        //Debug.Log(bot.statue);
        //aimTarget.Translate(new Vector3(h, 0, 0) * speed * Time.deltaTime);
        temp = h;
        if(now_move)
            Move();

        // if (h != 0 || v != 0)  // if we want to move and we are not hitting the ball
        // {
        //     targetPosition.x = ball.position.x; // update the target position to the ball's x position so the bot only moves on the x axis
        //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // lerp it's position
        // }
        
    }

    void Move()
    {

        targetPosition.x = ball.position.x; // update the target position to the ball's x position so the bot only moves on the x axis
        if(bal != null)
        {
            if(bal.botplay)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            } 

            // else
            //     transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // lerp it's position
        }
        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime); // lerp it's position
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Ball")
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir * (force / 1.1f);
            other.GetComponent<Rigidbody>().useGravity = true;
            //Debug.Log(speed);
            //Debug.Log(targets.Length);
            //aimTarget.Translate(new Vector3(temp, 0, 0) * speed * Time.deltaTime);
            animator.Play("forearm");
            now_move = true;
            if(bot != null)
            { 
                bot.statue = true;
                //Debug.Log("aa"+bot.statue);
            }
        }

        //ball.GetComponent<Ball>().hitter = "Paddle_one";
    }

    // public void reset()
    // {
    //     if(serveright)
    //         transform.position = serveLeft.position;

    //     else
    //         transform.position = serveRight.position;

            //serveright = !serveright;

    // }
}
