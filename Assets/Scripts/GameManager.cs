using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update

    #region shittodo
    //DO atleast one shit to do a day and your set
    //1.LEVEL SELECT --- remember to update with more levels when added
    //2.LEADERBOARD ----DONE BAMMMM, ahh shit sorta it dont exactly follow his specifications
    //3.THE PLAYER PREF SHIT----DONE 
    //4.COLLECTABLES-- DONE
    //5.MORE LEVELS-- goodish STILL NEED MORE
    //6.fireball sfx update sprite--update the powerup sprite itself i dont mind the blue fireballs though
    //7.brick break particle system --could axe this one dont really see it adding to game
    //8.learn what parralax is and try and do that cause it sounds rad--Still would be cool 
    //9.splash screens and shit-- yaaaa
    //12.Make it purtier---
    //11.THE FUCKING WRITEUP-done but update it
    //hitting esc from options will upause the game while still paused
    #endregion shittodo
    #region Attributes
        public bool inoptionsscreen=false;
    public bool inmain = false;
    [SerializeField]private GameObject turkey;
    public static GameManager instance = null;//declaration of singleton
    [SerializeField] private int lives = 3;//3 lives
    private MoveBall ball;//reference to ball script
    [SerializeField] private Text scoreTxt; //this is the textfield.
     private string preTextScore = "SCORE: ";//this is the default field
    private int score = 0;//the current score
    [SerializeField]private Text ammoTxt;
     private string preAmmotxt = "Ammo: "; 
    [SerializeField] private Text livesText;
    private string preLivesText = "Lives: ";
    [SerializeField]private int blocks = 0;//how many blocks are in the level;
    [SerializeField] private GameObject[] levels;//array of levels
    private static int currentLevel = 0; //Current level
    private GameObject currentBoard;//this is gameobject of the level
    [SerializeField] private int ammo = 0;
    public string prePreText = "";
    public GameObject btnRetry;
    public ParticleSystem boom;
    #region PowersAttributes
    public GameObject powerUp;
    public GameObject fireball;
    public GameObject player;
    public GameObject floor;
    public List<GameObject> activeCollectables = new List<GameObject>();
    public List<GameObject> activeFireballs = new List<GameObject>();//to prevent overlapping timers
    public List<GameObject> activeFloors = new List<GameObject>();
    public List<GameObject> activeSizePowers = new List<GameObject>();
    public List<GameObject> activePowers = new List<GameObject>();
    public List<GameObject> ActiveBalls = new List<GameObject>();
    [SerializeField]private GameObject deathzone;
    public bool deathquack = false;
    public float maxTimeScaler = 1.65f;
    #endregion PowersAttributes
    public bool inDeathScreen = false;
    [SerializeField] public bool paused = false;
    //Time Scale stuff
    [SerializeField] public float timeScaler=1;
    [SerializeField] public float fireballtime = 10f;
    private int bestScore = 0;//the highest score
   [SerializeField] public bool fireballon=false;
    public bool disableunpause = false;
    public int multiballs = 0;
    private bool gameovercheck = false;
    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }
    public int Lives { get => lives; set => lives = value; }
    public int Score { get => score; set => score = value; }
    public int Blocks { get => blocks; set => blocks = value; }
    public GameObject[] Levels { get => levels; set => levels = value; }
    public int Ammo { get => ammo; set => ammo = value; }
    #endregion Attributes
    #region Methods
    public void Shoot()
    {
        ammo--;
        UpdateAmmoUI();
        Vector2 fireballSpawn = new Vector2((player.transform.position.x), -3.25f);
        fireball.transform.position = fireballSpawn;
        Instantiate(fireball);

    }
    public void DestroyMultiballs()//gets rid of left over multiballs on level clear
    {
        for (int i = 0; i < ActiveBalls.Count; i++)
        {
                Destroy(ActiveBalls[i]);
            //ActiveBalls.RemoveAt(i);
           
        }
        multiballs = 0;
        ActiveBalls.Clear();
    }
    public void GameOver()
    {
        ball.gameObject.SetActive(false);
        UpdateScore(true);
        UpdateLives();
        if (Score > bestScore)
        {
            //Beaten Highscore 
            PlayerPrefs.SetInt("Score", Score);
            PlayerPrefs.Save();//will write the pref on the disk.
        }
        if (score > PlayerPrefs.GetInt("Highscores" + 9, 0))
        {
            ///THIS COULD BE CAUSING BUGS
            btnRetry.GetComponent<BtnBehaviour>().SetActiveHighScoreMenu(true);
            btnRetry.GetComponent<BtnBehaviour>().SetActiveDeathMenu(true);
            gameovercheck = true;
        }
        else
            btnRetry.GetComponent<BtnBehaviour>().SetActiveDeathMenu(true);
    }
 public void DestroyCollectables()
    {
        foreach(GameObject i in activeCollectables)
        {
            Destroy(i);
        }
        activeCollectables.Clear();
    }
    public void DestroyPowerUps(List<GameObject> list)//get rid of powerups on level clear
    {
        foreach(GameObject power in list)
        {
            Destroy(power);

        }
        list.Clear();
    }
    public void SpawnPowerup(GameObject brick)//Remember to change back the spawnrate
    {
        int powerchance = Random.Range(1, 101);
        int spawnrate = 30;
        if (powerchance <= spawnrate && blocks>1)
        {
            powerUp.transform.position = brick.transform.position;
            Instantiate(powerUp);

        }

    }
    public void spawncollectable(GameObject brick)
    {
        int spawnrate = 10;
        int powerchance = Random.Range(1, 101);
        if (powerchance <= spawnrate && blocks > 1)
        {
            turkey.transform.position = brick.transform.position;
            Instantiate(turkey);

        }
    }
    public void EnableFloor(bool t)//make your floor cooler
    {
        floor.GetComponent<SpriteRenderer>().enabled = t;
        floor.GetComponent<BoxCollider2D>().enabled = t;
    }
    public void RevertSize()
    {
        player.GetComponent<SpriteRenderer>().size = new Vector2(20.7f, 5.5f);
        player.GetComponent<BoxCollider2D>().size = new Vector2(20.7f, 5.6f);
    }
    public void PowerDown()//become a basic bitch paddle again
    {
        ammo = 0;
        EnableFloor(false);
        RevertSize();
        fireballon = false;
    }
    
    /// general game functions/////
    public void ResetTimeScale()
    {
        timeScaler = 1;
        Time.timeScale = 1;
    }
    //I found this method on the unity answers page posted by a user named salmjak modified it to fit my game
    //https://answers.unity.com/questions/1143629/destroy-multiple-gameobjects-with-tag-c.html
    public void FindAndDestroyProjectiles()
    {
        GameObject[] projectiles = GameObject.FindGameObjectsWithTag("Fireball");
         for (int i=0;i<projectiles.Length;i++)
        {
            Destroy(projectiles[i]);
        }
    }
    public void LoadLevel()
    {
        inDeathScreen = false;
        if (currentBoard)//if we have a board get rid of it
        {
            Destroy(currentBoard);
        }
        Blocks = 0;//resets the number of blocks
        currentBoard = Instantiate(Levels[CurrentLevel]);//Clones the Level.
        if (timeScaler == 0)
        {
            timeScaler = .95f;
        }
        timeScaler = timeScaler +.05f;//make game harder
        
        if(timeScaler>=maxTimeScaler)
        {
            timeScaler = maxTimeScaler;
        }
        
        Time.timeScale = timeScaler;
        PowerDown();
        DestroyMultiballs();
        DestroyPowerUps(activePowers);
        UpdateAmmoUI();
        DestroyCollectables();
        FindAndDestroyProjectiles();
    }
    public void UpdateAmmoUI()
    {
        ammoTxt.text = preAmmotxt + ammo.ToString("D1");
    }
    public void UpdateScore(bool gameover)
    {
        if (gameover == false)
        {
            scoreTxt.text = prePreText + preTextScore+ Score.ToString("D8");//"SCORE"+"00000000"
        }
        else 
        {
            scoreTxt.text = "Game Over \n" + prePreText+preTextScore + Score.ToString("D8");//"Game Over"\n"SCORE"+"00000000"
        }

    }
    public void UpdateLives()
    {
        livesText.text = preLivesText + Lives.ToString("D1");
    }
    public void Death()
    {
        Lives--;
        UpdateAmmoUI();
        PowerDown();
        if (Lives > 0)//still have lives
        {
            Init();//initialize game
            UpdateLives();

        }
        else
        {
            GameOver();
        }

    }
    public void AddPoints(int score)
    {
        Score += score;//adds 1000 to score 42o would be funnier

        if (Score > bestScore)
        {
            prePreText = "BEST ";
        }
        else
        {
            prePreText = "";
        }
        UpdateScore(false);

    }
  
    public void AddBlock()
    {
        Blocks++;
    }
    public void Init()
    {
        ball.gameObject.SetActive(true);
        ball.Init();
    }
    #endregion Methods

    /// //////////////////AWAKE////////////////////////

    private void Awake()
    {
        //only one instance exists
        if (instance==null)//if there is no instance this becomes instance
        {
            instance = this;
        }
        else if (instance !=this)
        {
            Destroy(gameObject);
        }
        

    }
    #region onlevelload
    //this would restart my level when loaded this wont be a thing soon so try not to use it
    //would also probably screw up a level select
    //private void OnLevelWasLoaded(int level)
    //{
    //    if (instance == null)//if there is no instance this becomes instance
    //    {
    //        instance = this;
    //    }
    //    else if (instance != this)
    //    {
    //        Destroy(gameObject);
    //    }

    //    ball = GameObject.Find("Ball").GetComponent<MoveBall>();
    //    LoadLevel();
    //    UpdateScore(false);
    //    UpdateLives();
    //    bestScore = PlayerPrefs.GetInt("Score", 0);//Read the old highscore
    //    btnRetry.GetComponent<BtnBehaviour>().SetActiveDeathMenu(false);
    //    btnRetry.GetComponent<BtnBehaviour>().SetActivePauseMenu(false);
    //    floor.GetComponent<BoxCollider2D>().enabled = false;
    //    floor.GetComponent<SpriteRenderer>().enabled = false;

    //}
    #endregion onlevelload
    void Start()
    {

        //find and get component moveball
        ball = GameObject.Find("Ball").GetComponent<MoveBall>();
        
        //optimize this code make a function
        LoadLevel();
        UpdateScore(false);
        UpdateLives();
        bestScore = PlayerPrefs.GetInt("Score",0);//Read the old highscore
        btnRetry.GetComponent<BtnBehaviour>().SetActiveDeathMenu(false);
        btnRetry.GetComponent<BtnBehaviour>().SetActivePauseMenu(false);
        GetComponent < BtnBehaviour >();
        floor.GetComponent<BoxCollider2D>().enabled = false;
        floor.GetComponent<SpriteRenderer>().enabled = false;
        floor=Instantiate(floor);
       
    }

  
    // Update is called once per frame ---------------------------
    void Update()
    {
       if (!paused &&!inDeathScreen)
        {
            Cursor.visible = false;
        }
       if (paused ||inDeathScreen)
        {
            Cursor.visible = true;
        }
        if (Input.GetButtonUp("Cancel") && !paused &&!inDeathScreen)//pause
        {
            Time.timeScale = 0;
            paused = true;
            btnRetry.GetComponent<BtnBehaviour>().SetActivePauseMenu(true);
            //Application.Quit();//hit esc quits game
        }
        else if (Input.GetButtonUp("Cancel") && paused &&!inDeathScreen &&!inoptionsscreen)//unpause
        {
            Time.timeScale = timeScaler;
            btnRetry.GetComponent<BtnBehaviour>().SetActivePauseMenu(false);
            paused = false;
           
        }
        if (!inmain)
        {


            if (Input.GetButtonDown("Service") && fireballon == true)//pewpew
            {
                Shoot();

            }
            if (inDeathScreen)
            {
                ammoTxt.enabled = false;
                livesText.enabled = false;
            }
            else
            {
                ammoTxt.enabled = true;
                livesText.enabled = true;
            }
            if (lives <= 0 && gameovercheck == false)
            {
                GameOver();
            }
            if (deathquack == true)
            {
                deathzone.GetComponent<AudioSource>().Play();
                deathquack = false;
            }
        }
    }

}
