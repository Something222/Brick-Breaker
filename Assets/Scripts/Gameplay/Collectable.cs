using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    void Start()
    {
       
        code = GameManager.instance;
        code.activeCollectables.Add(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (gameObject.tag == "Turkey")
            {
                code.AddPoints(5000);
                Destroy(gameObject);
            }
            if (gameObject.tag == "Golden Egg")
            {
                code.AddPoints(10000);
                code.Lives += 1;
                code.UpdateLives();
                Destroy(gameObject);
            }
        }
      
    }
    public void DestroyCollectables()
    {
        Destroy(gameObject);
        
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
