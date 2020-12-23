using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource track1;
    public AudioSource track2;
    public AudioSource track3;
    public GameManager code;
    //current track system is way better than this true and false bologna 
    //private bool track1done=false;
    //private bool track2done=false;
    int currenttrack;
    bool track1paused = false;
    bool track2paused = false;
    bool track3paused = false;
    void Start()
    {
        code = GameManager.instance;//recover gamemanager
        track1.Play();
        currenttrack = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (!track1.isPlaying &&currenttrack==1 &&track1paused==false)
        {
            currenttrack = 2;
            track2.Play();

        }
        if (!track2.isPlaying &&currenttrack==2 &&track2paused==false)
        {
            track3.Play();
            currenttrack = 3;

        }
        if (!track3.isPlaying && currenttrack == 3 && track3paused == false)
        {
            track1.Play();
            currenttrack = 1;

        }

        if (code.paused&&currenttrack==1)
        {
            track1.Pause();
            track1paused = true;
        }
        if (!code.paused &&track1paused==true)
        {
            track1.UnPause();
            track1paused = false;
        }
        if (code.paused && currenttrack == 2)
        {
            track2.Pause();
            track2paused = true;
        }
        if (!code.paused && track2paused == true)
        {
            track2.UnPause();
            track2paused = false;
        }
        if (code.paused && currenttrack == 3)
        {
            track3.Pause();
            track3paused = true;
        }
        if (!code.paused && track3paused == true)
        {
            track3.UnPause();
            track3paused = false;
        }

    }
}
