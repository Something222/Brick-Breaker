using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject poop;
    private float timer = 3.5f;
    private bool canpoop;
    private GameManager code;
    [SerializeField] private AudioSource[] quacks;
 
    void Start()
    {
        canpoop=true;
        code = GameManager.instance;
}
    public void Shootpoop()
    {

        quacks[1].PlayOneShot(quacks[1].clip);
        float poopspawnx = gameObject.transform.position.x;
        float poopspawny = gameObject.transform.position.y;
        poop.transform.position = new Vector2(poopspawnx, poopspawny - 1.2f);
        Instantiate(poop);
        canpoop = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Ball")
        {
            quacks[0].PlayOneShot(quacks[0].clip);
        }
    }
    private void OnDestroy()
    {
        code.deathquack = true;
        code.Score += 50000;
        code.UpdateScore(false);
    }
    // Update is called once per frame
    void Update()
    {
        if (canpoop)
        {
            
            canpoop = false;
            Invoke("Shootpoop", timer);
        }
    }
}
