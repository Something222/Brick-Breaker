using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngamePannelSelect : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject[] panels;
   
    // Start is called before the first frame update
    public void PanelToggle(int position)
    {
        Input.ResetInputAxes();//avoid double inputs
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(position == i);
          
        }
    }
    public void SavePrefs()
    {
        PlayerPrefs.Save();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
