using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turnoncursor : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    void Start()
    {
        code = GameManager.instance;
        code.paused = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
