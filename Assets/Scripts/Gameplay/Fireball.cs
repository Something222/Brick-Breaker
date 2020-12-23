using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    // Start is called before the first frame update
   
    void Start()
    {
        //code = GameManager.instance;
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

            Destroy(gameObject);
    }
}
