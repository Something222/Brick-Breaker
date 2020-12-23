using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;//reference to the gamemanager singleton
    private int life=1;
    [SerializeField] private Sprite postdamage;
    public void DestroyBlock(GameObject brick)
    {
        Destroy(brick);//brick commit suicide
        code.Blocks--; //Remove a block
        int powerchance = Random.Range(1, 101);
        code.SpawnPowerup(brick);
        code.spawncollectable(brick);

        if (code.Blocks <= 0)
        {
            if (code.CurrentLevel < code.Levels.Length - 1)
            {
                code.CurrentLevel++;//moves to the next level
            }
            else
            {
                code.CurrentLevel = 0;
                code.maxTimeScaler += 1;
            }
            code.Init();
            code.LoadLevel();
        }
    }

    void Start()
    {
      
        code = GameManager.instance;//gets the gamemanager into scene
        code.AddBlock();
        if (gameObject.tag == "WhiteBrick")
        {
            life = 2;
        }
        if (gameObject.tag=="Boss")
        {
            life = 6;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        code.timeScaler = Time.timeScale + .005f;
        //if(code.timeScaler>code.maxTimeScaler)
        //{
        //    code.timeScaler = code.maxTimeScaler;
        //}
        Time.timeScale = code.timeScaler;
        life--;
        
        code.AddPoints(1000);//increase point by 1k
        if (life <= 0)
        {
            DestroyBlock(gameObject);
            
        }
        if (gameObject.tag=="WhiteBrick")
        {
            GetComponent<SpriteRenderer>().sprite = postdamage;
            
        }
       
    }
}
