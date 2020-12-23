using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Board : MonoBehaviour
{
    [SerializeField]private AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag=="Ball")
        {
            audio.PlayOneShot(audio.clip);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
