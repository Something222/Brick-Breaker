using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
   
    [SerializeField] public float force = 150f;//force applied to force
    private Rigidbody2D ball; //Reference to component to rigidbody2d
    [SerializeField]private bool onlyOnce=true; //Service only once
    private int firstball;
   // private bool onlyOnce=false;
    private Transform myParent; //Linkto the original parent
    private Vector3 initPos;//local coordinate from the ball, relative to the parent
    private GameManager code;
  
   
    [SerializeField] private float blindspot = 0.2f;//half the blindspot
    [SerializeField] private float vForceMin = 0.6f;//minimum force on y axis before we prevent horizontal loop
    [SerializeField] private float vForceMultiplier = 2;//when we detect a horizontal bounce loop add this shit and stop that shit
    //private float yvel;
    // Start is called before the first frame update
    void Start()
    {
       
        code = GameManager.instance;//recover gamemanager
        ball = GetComponent<Rigidbody2D>(); //this recovers the component rigidbody2d
        
        myParent = transform.parent;//memorize the parent
        initPos = transform.localPosition;//Memorize the position of the ball relative to the parent
        if (code.multiballs == 0)
        {
            ball.simulated = false;//stops simulation
            onlyOnce = false;
            firstball = gameObject.GetInstanceID();

        }
        if(code.multiballs>0)
        {
            //if you want the multiballs to spawn 2 at once uncomment this
            //if (code.multiballs % 2 == 0)
            //{
                code.ActiveBalls.Add(gameObject);
                ball.AddForce(new Vector2(force, force));
            //}
            //else
            //{
            //    code.ActiveBalls.Add(gameObject);
            //    ball.AddForce(new Vector2(-force, force));
            //}
        }
    }
    public void Init()
    {
        transform.parent = myParent;//meet parent
        transform.localPosition = initPos;//Go back to where parent is now
        ball.simulated = false;
        ball.GetComponent<SpriteRenderer>().enabled = true;
        ball.GetComponent<CircleCollider2D>().enabled = true;
        ball.velocity = new Vector2(0, 0);//remove all the force
        onlyOnce = false;
    }
    // Update is called once per frame
    void Update()
    {
        //if i hit button service (spacebar) and havent done it yet
        if (Input.GetButtonUp("Service")&& !onlyOnce &&!code.inDeathScreen)
        {
            onlyOnce = true;//cant mash space bar and freeze ball
            ball.simulated = true;
            ball.transform.parent = null;//the ball will no longer be a child of the paddle basically wont follow
            ball.AddForce(new Vector2(force, force));
            //this may cause problems
           // yvel = ball.velocity.y;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        //prevents horizontal movement
        if (Mathf.Abs(ball.velocity.y)<vForceMin)//checks if were below that min threshold
        {
            float vecX = ball.velocity.x;//save existing x force.
            if (ball.velocity.y<0)//Going down
            {
                ball.velocity = new Vector2(vecX, -vForceMin * vForceMultiplier);//-1.2 force
            }
            else //going up
            {
                ball.velocity = new Vector2(vecX, vForceMin * vForceMultiplier);//1.2 force
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //this is abit more dynamic but the y speed is inconsistant
        //float yvel = ball.velocity.y;
        //Vector2 colPosition = collision.transform.position;
        //if (collision.gameObject.tag == "Paddle")
        //{

        //    float diffx = ((colPosition.x - transform.position.x) * 10);
        //    ball.velocity = new Vector2(-diffx, yvel);

        //}
        
        if (collision.gameObject.tag=="Brick"|| collision.gameObject.tag == "WhiteBrick")
        {
            GetComponent<AudioSource>().Play();
        }
        if (collision.gameObject.tag=="Death")
        {
            if (code.multiballs > 0)
            {
                code.multiballs--;
                if (gameObject.GetInstanceID() == firstball)
                {
                    code.boom.transform.position = gameObject.transform.position;
                    code.boom.Play();
                    code.boom.GetComponent<AudioSource>().Play();
                    ball.GetComponent<SpriteRenderer>().enabled = false;
                    ball.GetComponent<CircleCollider2D>().enabled = false;
                    ball.velocity = new Vector2(0, 0);
                   
                }
                else
                {
                    code.boom.transform.position = gameObject.transform.position;
                    code.boom.Play();
                    code.boom.GetComponent<AudioSource>().Play();
                    Destroy(gameObject);
                }  
            }
            else if (code.multiballs <= 0)
            {
                code.boom.transform.position = gameObject.transform.position;
                code.boom.Play();
                code.boom.GetComponent<AudioSource>().Play();
                code.Death();
                code.DestroyPowerUps(code.activePowers);
                if(gameObject.GetInstanceID() != firstball)
                {
                    Destroy(gameObject);
                }
                
            }
        }
        if (collision.gameObject.tag == "Paddle")
        {
            float diffx = transform.position.x - collision.transform.position.x;//Position of the ball-position of the paddle
            if (diffx < -blindspot)//left side of blindspot
            {
                ball.velocity = new Vector2(0, 0);
                ball.AddForce(new Vector2(-force, force));//(left, up)
            }
            else if (diffx > blindspot)//right side of blindspot
            {
                ball.velocity = new Vector2(0, 0);
                ball.AddForce(new Vector2(force, force));//(left, up)
            }

        }
       
    }
}
