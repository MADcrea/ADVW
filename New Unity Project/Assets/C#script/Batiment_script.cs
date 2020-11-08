using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Batiment_script : MonoBehaviour
{
    public enum BuildingType
    {
        Production,Unlock,Attack,Reload,Defense,Bonus,
    }
    public BuildingType type;
    public float health; public float H_limit; public float ammo; public float A_limit;
    public float reload; public float R_limit; 
    public float aim; public float Aim_limit; 
    public float spawn;
    public Vector3 Position; public Case_script Occupied_case;
    public bool activated;
    public string Effect;
    public int Owner;
    public GameObject UI_Man; UI_Manager_script UI_values;
    public GameObject HUD2GO; HUD2_display HUD2Value;

    public ResourceManager_script ResourceManager;

    // Start is called before the first frame update
    void Start()
    {
        activated = false;
        health =1;
        //Link to UI_Manager
        UI_Man = GameObject.Find("UI_Manager");
        UI_values = UI_Man.GetComponent<UI_Manager_script>();
        HUD2GO =GameObject.Find("Phase II");
        HUD2Value = HUD2GO.GetComponent<HUD2_display>();
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
     //Set up is called after unit instantiation
     public void set_up_batiment_value()
    {

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
    public void Change_Ammo_stock(float Ammo_change)
    {
        if(ammo+Ammo_change< A_limit)
        {
            ammo=ammo+Ammo_change;
        }
    }
    //Method called to pass spawn turn
    public void Change_time_before_activation()
    {
        if(spawn !=0)
        {
            spawn=spawn-1;
        }
        if(spawn==0)
        {
            activated=true;
            health=H_limit;
            ammo=A_limit;
            reload=R_limit;
            aim=Aim_limit;
        }
    }
    private void OnMouseUp() 
    {
        switch(UI_values.Step)
        {
            case UI_Manager_script.StepType.Intel:                      //Phase 2 Step Intel
                HUD2Value.Intel_UI_update(this.gameObject);
                break;
            case UI_Manager_script.StepType.DefineAttacker:             //Phase 2 Step Attack A
                if (type == BuildingType.Attack)
                {
                    HUD2Value.Attack_A_UI_update(this.gameObject);      //Phase 2 Step Attack B
                }
                break;
            case UI_Manager_script.StepType.DefineAttacked:
                    //Owner must be verified
                    //NOT a CITY must be verified
                HUD2Value.Attack_B_UI_update(this.gameObject);
                break;
            case UI_Manager_script.StepType.DefineSelfDestruct:
                string description =
                type                +"\n"+"\r"+ //type
                "Chance : 87%" + "" +"\n"+"\r"+ // proba fixe de 87% (ne pas faire 1 sur un dé à 6 faces)
                Owner               +"\n"+"\r"+ //Owner
                health              +"\n"+"\r"+ //health
                Effect ;
                HUD2Value.SD_UI_update("none",description,this.gameObject);
                break;
            default:
                break;
        }
    }
}
