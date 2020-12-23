using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LeaderBoard : MonoBehaviour
{
    // Start is called before the first frame update
    //Create the panel
    //--Find out how to load all the previous scores
    //create the check condition to see if panel pops 
    //

   [SerializeField] private Text[] nameorscore;
    public struct UserData
    {
       public string username;
        public int score;
    }
    public UserData[] scores=new UserData[10];
    private UserData playerstats;
    private GameManager code;
    void Start()
    {

        LoadStats();
        code = GameManager.instance;
    }
public void AddtoLeaderBoard (UserData playerdata)
    {
        int sub=0;
        bool check1=false;
        for (int i=9;i>=0; i--)
        {
            if (playerdata.score>scores[i].score)
            {
                check1 = true;
                sub = i;
            }
        }
        if (check1==true)
        {
            for (int i=8;i>sub;i--)
            {
                
                scores[i+1] = scores[i];
            }
           
            scores[sub] = playerdata;
        }
        for (int i=0;i<10;i++)
        {
            PlayerPrefs.SetInt("Highscores" + i, scores[i].score);
            PlayerPrefs.SetString("UserNames" + i, scores[i].username);
        }
        PlayerPrefs.Save();

    }
    public void FillUpTo10()
    {
    for (int i=0;i<10;i++)
        {
            if(scores[i].score==0)
            {
                scores[i].username = "aaa";
            }
            
        }
    }
    public void SetPlayerUserName(string name)
    {
        playerstats.username = name;
        playerstats.score = code.Score;
        AddtoLeaderBoard(playerstats);
    }
    public void SetPlayerScore()
    {
        playerstats.score= code.Score;
    }
    public void GenerateText(int i)
    {
        nameorscore[i].text = scores[i].username+"\t\t"+scores[i].score.ToString("D8");
    }
    public void LoadStats()
    {
        for (int i = 0; i < 10; i++)
        {

            scores[i].score = PlayerPrefs.GetInt("Highscores" + i, 0);
            scores[i].username = PlayerPrefs.GetString("UserNames" + i, "aaa");
            GenerateText(i);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
