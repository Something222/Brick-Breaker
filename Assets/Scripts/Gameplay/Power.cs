using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power : MonoBehaviour
{
    private GameManager code;
    private int floortime=10;
    private int bigtime = 10;
    [SerializeField]private GameObject ball;
    [SerializeField] private Sprite bigPowerSprite;
    [SerializeField] private Sprite smallPowerSprite;
    [SerializeField] private Sprite multiBallPowerSprite;
    [SerializeField] private Sprite floorpowerSprite;
    [SerializeField] private Sprite fireballPowerSprite;
    // Start is called before the first frame update
    void Start()
    {
        //if (powerType <100)
        code = GameManager.instance;
        code.activePowers.Add(gameObject);
        gameObject.GetComponent<CapsuleCollider2D>().isTrigger=true;
        int powerType = Random.Range(1, 101);
        //int powerType = 10;
        Debug.Log(powerType);
        if (powerType < 20)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = fireballPowerSprite;//fireball
            code.activeFireballs.Add(gameObject);
        }
        else if (powerType < 40 && powerType >= 20)
        {
            
            gameObject.GetComponent<SpriteRenderer>().sprite = floorpowerSprite;
            code.activeFloors.Add(gameObject);
        }
        else if (powerType < 60 && powerType >= 40)
        {
          //  gameObject.GetComponent<SpriteRenderer>().color = Color.green;//Big
            gameObject.GetComponent<SpriteRenderer>().sprite = bigPowerSprite;
            code.activeSizePowers.Add(gameObject);
        }
           // if (powerType <= 100)
        else if (powerType < 80 && powerType >= 60)
            {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;//MultiBall
            gameObject.GetComponent<SpriteRenderer>().sprite = multiBallPowerSprite;
            
            }
            else
            {
            //gameObject.GetComponent<SpriteRenderer>().color = Color.red;//small
            gameObject.GetComponent<SpriteRenderer>().sprite = smallPowerSprite;
            code.activeSizePowers.Add(gameObject);
            }




    }
    public IEnumerator BigPowerUp()
    {
        HidePowerup();
        code.player.GetComponent<SpriteRenderer>().size = new Vector2(29, 5.5f);
        code.player.GetComponent<BoxCollider2D>().size = new Vector2(29, 5.6f);
        yield return new WaitForSeconds(bigtime);
        code.RevertSize();
        Destroy(gameObject);
    }
    public IEnumerator SmallPowerDown()
    {
        HidePowerup();
        code.player.GetComponent<SpriteRenderer>().size = new Vector2(11.7f, 5.5f);
        code.player.GetComponent<BoxCollider2D>().size = new Vector2(11.7f, 5.6f);
        yield return new WaitForSeconds(bigtime);
        code.RevertSize();
        Destroy(gameObject);
    }
    public void MultiBallPowerUp()
    {
        //uncomment out the green if you want to spawn 2 multiballs comment the next line out 
        code.multiballs += 1;
        //code.multiballs+=2;
      
        ball.transform.localScale = new Vector3(1, 1, 1);
        Instantiate(ball);
        //instantiate(ball)

    }
    public void HidePowerup()
    {
        GetComponent<CapsuleCollider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
    public IEnumerator PowerUpFireball()
    {

        code.fireballon = true;
       
        code.Ammo = 3;
        //code.Ammo=5 
        code.UpdateAmmoUI();
        //Destroy(gameObject);
        HidePowerup();
        yield return new WaitForSeconds(code.fireballtime);
        code.fireballon = false;
        code.Ammo = 0;
        code.UpdateAmmoUI();
        Destroy(gameObject);


    }
    public IEnumerator PowerUpFloor()
    {
        
        HidePowerup();
        code.EnableFloor(true);
        yield return new WaitForSeconds(floortime);
        code.EnableFloor(false);
        Destroy(gameObject);
    }
    public void DestroyOldPowers(List<GameObject> list)
    {
        int stopPoint = 0;
        int i = 0;
        foreach (GameObject current in list)//this hopefully will get rid of the problem of interlooping timers SUCCESSS
        {
            if (current.GetInstanceID()==gameObject.GetInstanceID())
            {
                stopPoint = i;
            }
            i++;
        }
        for (int t = 0; t < stopPoint; t++)
        {
            Destroy(list[t]);
            //Destroy(list[0]);
            //list.RemoveAt(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (code.Ammo <= 0)
        {
            code.fireballon = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Paddle" )
        {
            if (gameObject.GetComponent<SpriteRenderer>().sprite == fireballPowerSprite)
            {
                
                StartCoroutine(PowerUpFireball());
                DestroyOldPowers(code.activeFireballs);
                //int stopPoint = 0;
                //int i = 0;
                //foreach (GameObject current in code.activeFireballs)//this hopefully will get rid of the problem of interlooping timers SUCCESSS
                //{
                //    if (current.GetInstanceID()==code.activeFireballs[i].GetInstanceID())
                //    {
                //        stopPoint = i;
                //    }
                //    i++;
                //}
                //for (int t=0; t<stopPoint;t++)
                //{
                //    Destroy(code.activeFireballs[t]);
                //}

            }
             if (gameObject.GetComponent<SpriteRenderer>().sprite == smallPowerSprite)
            {
                StartCoroutine(SmallPowerDown());
                DestroyOldPowers(code.activeSizePowers);
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == bigPowerSprite)
            {
                StartCoroutine(BigPowerUp());
                DestroyOldPowers(code.activeSizePowers);
            }
            else if (gameObject.GetComponent<SpriteRenderer>().sprite == multiBallPowerSprite)
            {
                Vector2 ballspawn = new Vector2((collision.transform.position.x), -3.25f);
                ball.transform.position = ballspawn;
                MultiBallPowerUp();
                Destroy(gameObject);
                
            }
            else if(gameObject.GetComponent<SpriteRenderer>().sprite == floorpowerSprite)
            {
                StartCoroutine(PowerUpFloor());
                DestroyOldPowers(code.activeFloors);
                
            }
        }
        else if (collision.gameObject.tag == "Death")
        {
            Destroy(gameObject);
        }
    }
}
