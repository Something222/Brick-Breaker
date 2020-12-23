using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    void Start()
    {
        code = GameManager.instance;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="Paddle")
        {
            code.Lives--;
            code.UpdateLives();
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Death")
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
