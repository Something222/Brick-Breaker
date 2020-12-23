using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]int rotation=0;
    private GameManager code;
    void Start()
    {
        code = GameManager.instance;
    }
   
    // Update is called once per frame
    void Update()
    {
        if (!code.paused)
        {
            rotation++;
        }
        if (rotation>=360)
        {
            rotation = 0;
        }
        transform.eulerAngles = new Vector3(0, 0, rotation);

       
    }
}
