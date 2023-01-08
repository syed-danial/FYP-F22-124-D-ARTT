using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    // public Vector3 initialImpulse;
    Rigidbody myRb;
    Vector3 initialPos;
    public bool playing = true;
    public bool again = false;
    public bool who;
    int check;
    public bool botplay;

    int playerScore;
    int botScore;

    //public TextMeshProUGUI playerScoreText1;
    [SerializeField] TextMeshProUGUI playerScoreText;
    [SerializeField] TextMeshProUGUI BotScoreText;
    //[SerializeField] TextMeshProUGUI NetHit;
    public TextMeshProUGUI NetHit;
    public TextMeshProUGUI OutHit;
    public TextMeshProUGUI OutHit1;

    void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.useGravity = false;
        initialPos = transform.position;
        //Debug.Log(transform.position.y);
        who = true;//player , 0 for bot
        playerScore = 0;
        botScore = 0;
        check = 0;
        botplay = false;
        //NetHit.text = " ";
        NetHit.gameObject.SetActive(false);
        OutHit.gameObject.SetActive(false);
        OutHit1.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position.x < -0.750 && transform.position.x < -0.750 && transform.position.y < -2.5 1.727 
        if (Input.GetKeyDown("f"))
        {
            myRb.useGravity = true;
        }

        // if(transform.position.x < -0.750)
        // {
        //     //Debug.Log("tra");
        //     check = 10;
        //     Again();
        // }

        // else if(transform.position.x > 1.657 && transform.position.y < -0.440)
        // {
        //     Debug.Log("tre");
        //     check = 30;
        //     Again();
        // }
            
        //     //Debug.Log("tra");
        

        if (transform.position.y < -40) {
            check = 20;
            //Debug.Log(transform.position.x);
            if(botplay)
            {
                check = 10;
                //botplay = false;
            }

            else
                check = 20;
            Again();
        }

        //Debug.Log(transform.position.x+"="+transform.position.y+"_"+transform.position.z);

        // if(transform.position.x < -0.750 && transform.position.x > 0.750 && transform.position.y < -2.5 && transform.position.z < 1.461 )
        // {
        //     //Debug.Log("tra");
        //     check = 10;
        //     Again();
        // }

        // else if(transform.position.x < -0.750 && transform.position.x > 0.750 && transform.position.y < -2.5 && transform.position.z > 4.600)
        // {
        //     Debug.Log("tre");
        //     check = 30;
        //     Again();
        // }
    }

    void Again()
    {
        transform.position = initialPos;
        myRb.useGravity = false;
        myRb.velocity = Vector3.zero;
        NetHit.gameObject.SetActive(false);
        OutHit.gameObject.SetActive(false);
        OutHit1.gameObject.SetActive(false);
        //NetHit.text = " ";
        // if(botplay)
        //     botplay = false;

        //Debug.Log(check);

        if(check == 10)
        {
            //Debug.Log("tra11");
            playerScore++;
            check = 0;
        }

        // else if(check == 30)
        // {
        //     //Debug.Log("tra11");
        //     botScore++;
        //     check = 0;
        // }

        else 
        {
            //Debug.Log("tra22");
            botScore++;
            check = 0;
        }
        //again = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Debug.Log(collision.gameObject.name);

        if (collision.gameObject.name == "Paddle_one")
        {
            //If the GameObject's name matches the one you suggest, output this message in the console
            myRb.useGravity = true;
            botplay = false;
            
        }
        

        else if (collision.gameObject.name == "NET") // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            Debug.Log(botplay);
            if(botplay)
            {
                check = 10;
                //botplay = false;
                //NetHit.text = "Net Hitted !!";
                //Debug.Log("aa1");
            }

            else
            {
                check = 20;
                //NetHit.text = "Net Hitted !!";
                //Debug.Log("aa2");
            }
            //WaitForSeconds();
            StartCoroutine(WaitBeforeShow1());
            //Again(); // reset it's position 
        }

        else if (collision.gameObject.name == "PlayerOut") // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            check = 10;
            Debug.Log("asasHH");
            StartCoroutine(WaitBeforeShow3());
            //Again(); // reset it's position 
        }

        else if (collision.gameObject.name == "BotOut") // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            check = 20;
            
            StartCoroutine(WaitBeforeShow2());
            //Again(); // reset it's position 
        }

        else if (collision.gameObject.name == "Side_1") // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            check = 20;
            StartCoroutine(WaitBeforeShow2());
            //Again(); // reset it's position 
        }

        else if (collision.gameObject.name == "Side_2") // if the ball hits a wall
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; // reset it's velocity to 0 so it doesn't move anymore
            check = 20;
            StartCoroutine(WaitBeforeShow2());
            //Again(); // reset it's position 
        }
        updateScores();

        //if(collision.gameObject.name == "NET")
        //GameObject.Find("Paddle_one").GetComponent<Paddle_one>().Reset();


        // if(playing)
        // {
            // if(hitter == "bot")
        // {
        //     botScore++;
        // }

        // else if(hitter == "player")
        // {
        //     playerScore++;
        // }
        // }
        
        
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.CompareTag("Out"))
    //     {
    //        if(hitter == "player")
    //        {
    //         botScore++;
    //        }

    //        if(hitter == "player")
    //        {
    //         playerScore++;
    //        }
    //     }

    // }    

    // public IEnumerator GravityDisableRoutine()
    // {
    //     myRb.useGravity = false;
    //     yield return new WaitForSeconds(10); //You may change this number of seconds
    //     myRb.useGravity = true;
    // }

    void updateScores()
    {
        playerScoreText.text = "Player : " + playerScore;
        BotScoreText.text = "Bot : " + botScore;
    }

    IEnumerator WaitBeforeShow1()
    {
        NetHit.gameObject.SetActive(true);
    
        yield return new WaitForSeconds(2);
        //NetHit.gameObject.SetActive(true);
        Again();
    
    }

    IEnumerator WaitBeforeShow2()
    {
        OutHit.gameObject.SetActive(true);
    
        yield return new WaitForSeconds(2);
        //NetHit.gameObject.SetActive(true);
        Again();
    
    }

    IEnumerator WaitBeforeShow3()
    {
        OutHit1.gameObject.SetActive(true);
    
        yield return new WaitForSeconds(2);
        //NetHit.gameObject.SetActive(true);
        Again();
    
    }
}
