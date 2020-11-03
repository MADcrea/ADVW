using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batiment_script : MonoBehaviour
{
    public string type;
    public float health;
    public float time_before_activation;
    public Vector3 Position;
    public string Case;
    public string Effect;
    public int Owner;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
     //Set up is called after unit instantiation
     public void set_up_batiment_value(){

    }
    //Method called at building an new unit
    public void Create_Unit()
    {

    }
    //Method called for taking damage or healing
    public void Change_Health_stock(float Health_change)
    {
        health=health+Health_change;
        if(health<=0)
        {
            Destroy(this.gameObject);
            Debug.Log(name + " from owner " + Owner +" has been killed");
        }
    }
    public void Change_Ammo_stock(float Ammo_change){}
    //Method called to pass spawn turn
    public void Change_time_before_activation()
    {

    }
    private void OnMouseUp() 
    {
         //Link to UI_Manager
        GameObject UI_Man = GameObject.Find("UI_Manager");
        UI_Manager_script UI_values = UI_Man.GetComponent<UI_Manager_script>();

        //Phase 2 Step Intel
        if (UI_values.Phase_number ==2 && UI_values.Step == UI_Manager_script.StepType.Intel)
        {
            //Link to intel_display
            GameObject HUD2= GameObject.Find("Phase II");
            HUD2_display HUD = HUD2.GetComponent<HUD2_display>();
            HUD.Intel_UI_update(this.gameObject);
        }

        //Phase 2 Step Attack A
        // Owner Must be verified
        // Attack type must be verified
        if (UI_values.Phase_number ==2 && UI_values.Step == UI_Manager_script.StepType.Attack_A)
        {
            //Link to intel_display
            GameObject HUD2= GameObject.Find("Phase II");
            HUD2_display HUD = HUD2.GetComponent<HUD2_display>();
            string description =
            type                +"\n"+"\r"+ //type
            Owner               +"\n"+"\r"+ //Owner
            health              +"\n"+"\r"+ //health
            Effect ;

            HUD.Attack_A_UI_update("none",description,this.gameObject);
        }

        //Phase 2 Step Attack B
        //Owner must be verified
        //NOT a CITY must be verified
        if (UI_values.Phase_number ==2 && UI_values.Step == UI_Manager_script.StepType.Attack_B)
        {
            //Link to intel_display
            GameObject HUD2= GameObject.Find("Phase II");
            HUD2_display HUD = HUD2.GetComponent<HUD2_display>();
            string description =
            type                +"\n"+"\r"+ //type
            "Chance : "+""      +"\n"+"\r"+ // Calcul des proba de réussite
            Owner               +"\n"+"\r"+ //Owner
            health              +"\n"+"\r"+ //health
            Effect ;

            HUD.Attack_B_UI_update("none",description,this.gameObject);
        }

        //Phase 2 Step Self-Destruct
        //Owner must be verified
        if (UI_values.Phase_number ==2 && UI_values.Step == UI_Manager_script.StepType.SD_A)
        {
            //Link to intel_display
            GameObject HUD2= GameObject.Find("Phase II");
            HUD2_display HUD = HUD2.GetComponent<HUD2_display>();
            string description =
            type                +"\n"+"\r"+ //type
            "Chance : 87%" + "" +"\n"+"\r"+ // proba fixe de 87% (ne pas faire 1 sur un dé à 6 faces)
            Owner               +"\n"+"\r"+ //Owner
            health              +"\n"+"\r"+ //health
            Effect ;

            HUD.SD_UI_update("none",description,this.gameObject);
        }
    }
}
