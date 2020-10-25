using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Manager_script : MonoBehaviour
{
    public int Phase_number;
    public StepType Step;

    public enum StepType
    {
        Intel,
        //...
    }
    public string Player_turn;
    // Start is called before the first frame update
    void Start()
    {
        Phase_number=2;
        Step="none";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
