using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SuperSecretScript : MonoBehaviour
{
    [SerializeField] private AudioSource coolsound;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void playcoolsound()
    {
        coolsound.PlayOneShot(coolsound.clip);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
