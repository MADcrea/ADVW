using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD1_script : MonoBehaviour
{
    public GameObject UI_Man;

    // Start is called before the first frame update
    void Start()
    {
        //Link to UI manager
        UI_Man = GameObject.Find("UI_Manager");
        UI_Manager_script UI_values = UI_Man.GetComponent<UI_Manager_script>();

        //Link to UI canvas to display/hide
        /* Intel_UI = PH2_Intel.GetComponent<CanvasGroup>();
        Attack_UI = PH2_Attack.GetComponent<CanvasGroup>();
        Move_UI = PH2_Move.GetComponent<CanvasGroup>();
        SD_UI = PH2_SD.GetComponent<CanvasGroup>(); */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
