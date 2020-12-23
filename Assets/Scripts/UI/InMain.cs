using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InMain : MonoBehaviour
{
    // Start is called before the first frame update
    private GameManager code;
    void Start()
    {
        code = GameManager.instance;
        code.inmain = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
