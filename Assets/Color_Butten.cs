using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Color_Butten : MonoBehaviour
{
    public string Color_Name;

    public Game_Controller Game_Controller;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Select_This_Color()
    {
        Game_Controller.Check_The_Color(Color_Name);
    }
}
