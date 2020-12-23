using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovePaddle : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject layer1;
    [SerializeField] private GameObject layer2;
    [SerializeField] private GameObject layer3;
    public GameManager code;
    private float layer1limit= 0.0435f;
    private float layer2limit= 0.018f;
    private float layer3limit=0.01f;
    //  [SerializeField] private float positionLimit = 2.2f;
    // Start is called before the first frame update
    void Start()
    {
        code = GameManager.instance;
        //slider.enabled = false;//this will disable the slider enable it for the mobile version
    }
   [SerializeField] public static float speed = 5; //sensitivity for the mouse maybe we can add a menu to adjust this value
    // Update is called once per frame
    
    void Update()
    {
        //i dont like the slider
        //now its there but hidden 
        //I can just get rid of it but the instructions say to have one plus this way i just need 
        //to make it revisible for the mobile version.
        //i wont like it there either though... oh well
        float pos = slider.value;
        if (!code.paused &&!code.inDeathScreen)
        { 
        slider.value= (Input.GetAxis("Mouse X") * speed) * Time.deltaTime / Time.timeScale;
        
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal")>0)
        {
            slider.value += .1f;
        }
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            slider.value -= .1f;
            
        }
        layer1.transform.Translate(pos*.025f, 0, 0);
        layer2.transform.Translate(pos * .010f, 0, 0);
        layer3.transform.Translate(pos * .005f, 0, 0);
        transform.Translate(pos, 0, 0);
        
        
            if (layer1.transform.position.x < -layer1limit)
            {
                layer1.transform.position = new Vector3(-layer1limit, .14f, 1.94f);
            }
            if (layer1.transform.position.x > layer1limit)
            {
                layer1.transform.position = new Vector3(layer1limit, .14f, 1.94f);
            }
            if (layer2.transform.position.x > layer2limit)
            {
                layer2.transform.position = new Vector3(layer2limit, .14f, 1.94f);
            }
            if (layer2.transform.position.x < -layer2limit)
            {
                layer2.transform.position = new Vector3(-layer2limit, .14f, 1.94f);
            }
            if (layer3.transform.position.x > layer3limit)
            {
                layer3.transform.position = new Vector3(layer3limit, .14f, 1.94f);
            }
            if (layer3.transform.position.x < -layer3limit)
            {
                layer3.transform.position = new Vector3(-layer3limit, .14f, 1.94f);
            }
            // if (layer1.transform.position.x<)

            if (gameObject.transform.position.x > 1.9f)
            {
                gameObject.transform.position = new Vector2(1.9f, -3.75f);
            }
            if (gameObject.transform.position.x < -1.9f)
            {
                gameObject.transform.position = new Vector2(-1.9f, -3.75f);
            }
        }
        //This will enable slider controls
        // float slidepos = slider.value;
        //transform.position = new Vector2(pos * positionLimit, transform.position.y);// this enables slider controls
    }
}
